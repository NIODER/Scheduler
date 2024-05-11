using MapsterMapper;
using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Problems.Common;
using Scheduler.Domain.ProblemAggregate;

namespace Scheduler.Application.Problems.Queries.GetUserProblems;

internal class GetUserProblemsQueryHandler(IProblemsRepository problemsRepository, IMapper mapper) 
    : IRequestHandler<GetUserProblemsQuery, UserProblemsResult>
{
    private readonly IProblemsRepository _problemsRepository = problemsRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<UserProblemsResult> Handle(GetUserProblemsQuery request, CancellationToken cancellationToken)
    {
        List<Problem> problems = await _problemsRepository.GetProblemsCreatedOrAssignedToUserByUserIdAsync(new(request.UserId));
        return new UserProblemsResult(new(request.UserId), _mapper.Map<List<ProblemResult>>(problems));
    }
}
