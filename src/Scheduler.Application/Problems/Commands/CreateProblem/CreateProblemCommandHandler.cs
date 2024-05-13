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
using Scheduler.Domain.UserAggregate;

namespace Scheduler.Application.Problems.Commands.CreateProblem;

internal class CreateProblemCommandHandler(
    IProblemsRepository problemsRepository,
    IGroupsRepository groupsRepository,
    IUsersRepository usersRepository,
    IDateTimeProvider dateTimeProvider,
    ILogger<CreateProblemCommandHandler> logger,
    IMapper mapper) : IRequestHandler<CreateProblemCommand, ICommandResult<ProblemResult>>
{
    private readonly IProblemsRepository _problemsRepository = problemsRepository;
    private readonly IGroupsRepository _groupsRepository = groupsRepository;
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;
    private readonly ILogger<CreateProblemCommandHandler> _logger = logger;
    private readonly IMapper _mapper = mapper;

    public async Task<ICommandResult<ProblemResult>> Handle(CreateProblemCommand request, CancellationToken cancellationToken)
    {
        if (request.GroupId is null && request.UserId is not null)
        {
            return new InvalidData<ProblemResult>($"Can not assign another user if task is not assigned to group.");
        }
        if (request.Deadline < _dateTimeProvider.UtcNow)
        {
            return new InvalidData<ProblemResult>($"Deadline could not be in past.");
        }
        User? taskCreator = await _usersRepository.GetUserByIdAsync(new(request.CreatorId));
        User? executor = null;
        Group? group = null;
        if (taskCreator is null)
        {
            return new NotFound<ProblemResult>($"User with id {request.CreatorId} was not found.");
        }
        if (request.GroupId is not null)
        {
            group = await _groupsRepository.GetGroupByIdAsync(new(request.GroupId.Value));
            if (group is null)
            {
                return new InvalidData<ProblemResult>($"Group is not found");
            }
            GroupUser? groupUser = group.Users.SingleOrDefault(u => u.UserId.Value == request.CreatorId);
            if (groupUser is null)
            {
                return new AccessViolation<ProblemResult>($"Not a member.");
            }
            if (!groupUser.Permissions.HasFlag(UserGroupPermissions.CreateTask))
            {
                return new AccessViolation<ProblemResult>($"No permissions.");
            }
            if (request.UserId is not null)
            {
                executor = await _usersRepository.GetUserByIdAsync(new(request.UserId.Value));
                GroupUser? groupExecutor = group.Users.SingleOrDefault(u => u.UserId.Value == request.UserId);
                if (groupExecutor is null)
                {
                    return new InvalidData<ProblemResult>("Assigned user is not in group.");
                }
            }
        }
        var problem = Problem.Create(
            taskCreator.Id,
            executor?.Id,
            group?.Id,
            request.Title,
            request.Description,
            request.Deadline,
            status: request.Status.HasValue ? (ProblemStatus)request.Status.Value : ProblemStatus.New);
        _problemsRepository.Add(problem);
        await _problemsRepository.SaveChangesAsync();
        _logger.LogInformation("Task {taskId} created.", problem.Id.Value);
        taskCreator.AddProblem(problem.Id);
        _usersRepository.Update(taskCreator);
        if (executor is not null)
        {
            executor.AddProblem(problem.Id);
            _usersRepository.Update(executor);
        }
        await _usersRepository.SaveChangesAsync();
        _logger.LogInformation("Task {taskId} added to users.", problem.Id.Value);
        var result = _mapper.Map<ProblemResult>(problem);
        return new SuccessResult<ProblemResult>(result);
    }
}
