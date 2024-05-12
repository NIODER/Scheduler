using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Problems.Common;
using Scheduler.Domain.GroupAggregate;
using Scheduler.Domain.ProblemAggregate;

namespace Scheduler.Application.Problems.Queries.GetGroupProblems;

internal class GetGroupProblemsCommandHandler(
    IGroupsRepository groupsRepository,
    IProblemsRepository problemsRepository,
    IMapper mapper,
    ILogger<GetGroupProblemsCommandHandler> logger)
    : IRequestHandler<GetGroupProblemsCommand, ICommandResult<GroupProblemsResult>>
{
    private readonly IGroupsRepository _groupsRepository = groupsRepository;
    private readonly IProblemsRepository _problemsRepository = problemsRepository;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<GetGroupProblemsCommandHandler> _logger = logger;

    public async Task<ICommandResult<GroupProblemsResult>> Handle(GetGroupProblemsCommand request, CancellationToken cancellationToken)
    {
        Group? group = await _groupsRepository.GetGroupByIdAsync(new(request.GroupId));
        if (group is null)
        {
            return new NotFound<GroupProblemsResult>($"No group with id {request.GroupId} found.");
        }
        if (!group.Users.Any(u => u.UserId.Value == request.UserId))
        {
            return new AccessViolation<GroupProblemsResult>("Not in group.");
        }
        List<Problem> problems = await _problemsRepository.GetGroupProblemsByGroupIdAsync(group.Id);
        var result = new GroupProblemsResult(
            group.Id,
            _mapper.Map<List<ProblemResult>>(problems));
        return new SuccessResult<GroupProblemsResult>(result);
    }
}
