using MapsterMapper;
using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Problems.Common;
using Scheduler.Domain.GroupAggregate;
using Scheduler.Domain.ProblemAggregate;

namespace Scheduler.Application.Problems.Queries.GetGroupProblems;

internal class GetGroupProblemsCommandHandler(IGroupsRepository groupsRepository, IProblemsRepository problemsRepository, IMapper mapper)
    : IRequestHandler<GetGroupProblemsCommand, AccessResultWrapper<GroupProblemsResult>>
{
    private readonly IGroupsRepository _groupsRepository = groupsRepository;
    private readonly IProblemsRepository _problemsRepository = problemsRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<AccessResultWrapper<GroupProblemsResult>> Handle(GetGroupProblemsCommand request, CancellationToken cancellationToken)
    {
        Group group = await _groupsRepository.GetGroupByIdAsync(new(request.GroupId))
            ?? throw new NullReferenceException($"No group with id {request.GroupId} found.");
        if (!group.Users.Any(u => u.UserId.Value == request.UserId))
        {
            return AccessResultWrapper<GroupProblemsResult>.CreateForbidden();
        }
        List<Problem> problems = await _problemsRepository.GetGroupProblemsByGroupIdAsync(group.Id);
        var result = new GroupProblemsResult(
            group.Id,
            _mapper.Map<List<ProblemResult>>(problems));
        return AccessResultWrapper<GroupProblemsResult>.Create(result);
    }
}
