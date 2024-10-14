using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Interfaces.Services;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Problems.Common;
using Scheduler.Domain.GroupAggregate;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.ProblemAggregate;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Application.Problems.Commands.UpdateProblem;

internal class UpdateProblemCommandHandler(
    IProblemsRepository problemsRepository,
    IGroupsRepository groupsRepository,
    ILogger<UpdateProblemCommandHandler> logger,
    IDateTimeProvider dateTimeProvider,
    IMapper mapper) : IRequestHandler<UpdateProblemCommand, ICommandResult<ProblemResult>>
{
    private readonly IProblemsRepository _problemsRepository = problemsRepository;
    private readonly IGroupsRepository _groupsRepository = groupsRepository;
    private readonly ILogger<UpdateProblemCommandHandler> _logger = logger;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;
    private readonly IMapper _mapper = mapper;

    public async Task<ICommandResult<ProblemResult>> Handle(UpdateProblemCommand request, CancellationToken cancellationToken)
    {
        Problem? problem = await _problemsRepository.GetProblemByIdAsync(new(request.ProblemId));
        if (problem is null)
        {
            return new NotFound<ProblemResult>($"Task with id {request.ProblemId} was not found.");
        }
        if (problem.IsAssignedToGroup)
        {
            Group? group = await _groupsRepository.GetGroupByIdAsync(problem.GroupId!);
            if (!GroupExists(group, problem, out var error))
            {
                return error!;
            }
            if (!ExecutorIsMemberAndHasPermissions(group!, new(request.ExecutorId), out error))
            {
                return error!;
            }
            if (!AssignedUserIsMember(group!, request.UserId, out error))
            {
                return error!;
            }
            if (!AssignedUserUpdatedSuccessfully(request, problem, group!, out error))
            {
                return error!;
            }
        }
        if (!DeadlineIsValid(request))
        {
            return new InvalidData<ProblemResult>("Deadline cannot be in past.");
        }
        UpdateProblem(request, problem);
        await _problemsRepository.SaveChangesAsync();
        var result = _mapper.Map<ProblemResult>(problem);
        return new SuccessResult<ProblemResult>(result);
    }

    private bool GroupExists(Group? group, Problem problem, out IErrorResult<ProblemResult>? error)
    {
        if (group is null)
        {
            _logger.LogError(
                "No group with id {groupId} found, but problem {problemId} is attached to it.",
                problem.GroupId!.Value,
                problem.Id.Value);
            error = new InternalError<ProblemResult>();
            return false;
        }
        error = null;
        return true; ;
    }

    private static bool ExecutorIsMemberAndHasPermissions(Group group, UserId userId, out IErrorResult<ProblemResult>? error)
    {
        var groupUser = group.Users.SingleOrDefault(u => u.UserId == userId);
        if (groupUser == default)
        {
            error = new AccessViolation<ProblemResult>("User is not a member.");
            return false;
        }
        if (!groupUser!.Permissions.HasFlag(UserGroupPermissions.ChangeTask))
        {
            error = new AccessViolation<ProblemResult>("User has no permissions.");
            return false;
        }
        error = null;
        return true;
    }

    private static bool AssignedUserIsMember(Group group, Guid? userId, out IErrorResult<ProblemResult>? error)
    {
        if (!userId.HasValue)
        {
            error = null;
            return true;
        }
        var groupUser = group.Users.SingleOrDefault(u => u.UserId.Value == userId.Value);
        if (groupUser == default)
        {
            error = new AccessViolation<ProblemResult>("Assigned user is not a member.");
            return false;
        }
        error = null;
        return true;
    }

    private bool AssignedUserUpdatedSuccessfully(UpdateProblemCommand request, Problem problem, Group group, out IErrorResult<ProblemResult>? error)
    {
        if (request.UserId is null)
        {
            UnassignUser(request, problem);
        }
        else
        {
            error = AssignUserOrGetErrorResult(request.UserId.Value, group!, problem);
            return false;
        }
        error = null;
        return true;
    }

    private void UnassignUser(UpdateProblemCommand request, Problem problem)
    {
        if (request.UserId is not null)
        {
            _logger.LogDebug("Unassign user {assignedUserId} from problem {problemId}", problem.UserId, problem.Id);
            problem.UnassignUserFromProblem();
        }
    }

    private static InternalError<ProblemResult>? AssignUserOrGetErrorResult(Guid userId, Group group, Problem problem)
    {
        GroupUser? assignedUser = group!.Users.SingleOrDefault(u => u.UserId.Value == userId);
        if (assignedUser is null)
        {
            return new InternalError<ProblemResult>(
                message: $"User with id {userId} was not found in group.");
        }
        try
        {
            problem.AssignUserToProblem(assignedUser.UserId);
        }
        catch (InvalidOperationException e)
        {
            return new InternalError<ProblemResult>(message: e.Message);
        }
        return null;
    }

    private bool DeadlineIsValid(UpdateProblemCommand request)
    {
        return request.Deadline > _dateTimeProvider.UtcNow;
    }

    private void UpdateProblem(UpdateProblemCommand request, Problem problem)
    {
        problem.Title = request.Title;
        problem.Description = request.Description;
        problem.Status = request.Status is null ? ProblemStatus.New : (ProblemStatus)request.Status;
        problem.Deadline = request.Deadline;
        _problemsRepository.Update(problem);
    }
}
