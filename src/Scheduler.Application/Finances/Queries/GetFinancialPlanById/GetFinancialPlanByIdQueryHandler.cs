using MapsterMapper;
using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Finances.Common;

namespace Scheduler.Application.Finances.Queries.GetFinancialPlanById;

internal class GetFinancialPlanByIdQueryHandler(
    IUsersRepository usersRepository,
    IFinancialPlansRepository financialPlansRepository,
    IMapper mapper) : IRequestHandler<GetFinancialPlanByIdQuery, ICommandResult<FinancialPlanResult>>
{
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly IFinancialPlansRepository _financialPlansRepository = financialPlansRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ICommandResult<FinancialPlanResult>> Handle(GetFinancialPlanByIdQuery request, CancellationToken cancellationToken)
    {
        var financialPlan = await _financialPlansRepository.GetFinancialPlanByIdAsync(new(request.FinancialId));
        if (financialPlan is null)
        {
            return new NotFound<FinancialPlanResult>($"No financial plan with id {request.FinancialId} was found.");
        }

        if (financialPlan.GroupId is null)
        {
            if (financialPlan.CreatorId.Value != request.UserId)
            {
                return new AccessViolation<FinancialPlanResult>($"User with id {request.UserId} has no access to this financial plan.");
            }
        }
        else
        {
            var user = await _usersRepository.GetUserByIdAsync(new(request.UserId));
            if (user is null)
            {
                return new NotFound<FinancialPlanResult>($"No user with id {request.UserId} was found.");
            }

            if (!user.GroupIds.Any(g => g == financialPlan.GroupId))
            {
                return new AccessViolation<FinancialPlanResult>($"User with id {request.UserId} has no access to this financial plan.");
            }
        }

        return new SuccessResult<FinancialPlanResult>(_mapper.Map<FinancialPlanResult>(financialPlan));
    }
}
