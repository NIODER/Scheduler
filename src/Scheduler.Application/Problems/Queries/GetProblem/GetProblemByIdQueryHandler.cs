using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Problems.Common;
using Scheduler.Domain.GroupAggregate;
using Scheduler.Domain.ProblemAggregate;

namespace Scheduler.Application.Problems.Queries.GetProblem;

internal class GetProblemByIdQueryHandler(
    IProblemsRepository problemsRepository,
    IGroupsRepository groupsRepository,
    ILogger<GetProblemByIdQueryHandler> logger,
    IMapper mapper) : IRequestHandler<GetProblemByIdQuery, ICommandResult<ProblemResult>>
{
    private readonly IProblemsRepository _problemsRepository = problemsRepository;
    private readonly IGroupsRepository _groupsRepository = groupsRepository;
    private readonly ILogger<GetProblemByIdQueryHandler> _logger = logger;
    private readonly IMapper _mapper = mapper;

    public async Task<ICommandResult<ProblemResult>> Handle(GetProblemByIdQuery request, CancellationToken cancellationToken)
    {
        Problem? problem = await _problemsRepository.GetProblemByIdAsync(new(request.TaskId));
        if (problem is null)
        {
            return new NotFound<ProblemResult>();
        }
        if (problem.CreatorId.Value != request.ExecutorId && problem.UserId?.Value != request.ExecutorId)
        {
            if (problem.GroupId is null)
            {
                return new AccessViolation<ProblemResult>("No permissions.");
            }
            Group? group = await _groupsRepository.GetGroupByIdAsync(problem.GroupId);
            if (group is null)
            {
                var exception = new NullReferenceException($"Group with id {problem.GroupId.Value} wasn't found");
                _logger.LogError(
                    exception, 
                    "Task {taskId} is attached to group {groupId}. Group with wasn't found.",
                    request.TaskId,
                    problem.GroupId.Value);
                return new InternalError<ProblemResult>(exception, message: "Task attached to group. Group wasn't found.");
            }
            if (!group.Users.Any(u => u.UserId.Value == request.ExecutorId))
            {
                return new AccessViolation<ProblemResult>("Isn't a member.");
            }
        }
        var result = _mapper.Map<ProblemResult>(problem);
        return new SuccessResult<ProblemResult>(result);
    }
}
