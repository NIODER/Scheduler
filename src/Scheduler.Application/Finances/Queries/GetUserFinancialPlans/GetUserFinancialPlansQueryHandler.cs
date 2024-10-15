using MapsterMapper;
using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Finances.Common;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Application.Finances.Queries.GetUserFinancialPlans;

internal class GetUserFinancialPlansQueryHandler(
    IUsersRepository usersRepository,
    IFinancialPlansRepository financialPlansRepository,
    IMapper mapper) : IRequestHandler<GetUserFinancialPlansQuery, ICommandResult<UserFinancialPlansListResult>>
{
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly IFinancialPlansRepository _financialPlansRepository = financialPlansRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ICommandResult<UserFinancialPlansListResult>> Handle(GetUserFinancialPlansQuery request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetUserByIdAsync(new UserId(request.UserId));
        if (user is null)
        {
            return new NotFound<UserFinancialPlansListResult>();
        }
        if (user.FinancialPlanIds.Count == 0)
        {
            return new SuccessResult<UserFinancialPlansListResult>(new UserFinancialPlansListResult(0, []));
        }

        var userFinancialPlans = await _financialPlansRepository.GetPrivateFinancialPlansByUserIdAsync(user.Id);
        var userFinancialPlansResult = _mapper.Map<List<FinancialPlanResult>>(userFinancialPlans);
        var result = new UserFinancialPlansListResult(userFinancialPlans.Count, userFinancialPlansResult);

        return new SuccessResult<UserFinancialPlansListResult>(result);
    }
}
