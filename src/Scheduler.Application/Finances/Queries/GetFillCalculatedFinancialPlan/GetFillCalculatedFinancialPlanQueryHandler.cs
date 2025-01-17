using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Finances.Common;
using Scheduler.Domain.FinancialPlanAggregate.Calculation;

namespace Scheduler.Application.Finances.Queries.GetFillCalculatedFinancialPlan
{
    internal class GetFillCalculatedFinancialPlanQueryHandler(
        IUsersRepository usersRepository,
        IFinancialPlansRepository financialPlansRepository,
        IGroupsRepository groupsRepository,
        IMapper mapper,
        ILogger<GetFillCalculatedFinancialPlanQueryHandler> logger) : IRequestHandler<GetFillCalculatedFinancialPlanQuery, ICommandResult<FilledFinancialPlanResult>>
    {
        private readonly IUsersRepository _usersRepository = usersRepository;
        private readonly IFinancialPlansRepository _financialPlansRepository = financialPlansRepository;
        private readonly IGroupsRepository _groupsRepository = groupsRepository;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<GetFillCalculatedFinancialPlanQueryHandler> _logger = logger;

        public async Task<ICommandResult<FilledFinancialPlanResult>> Handle(GetFillCalculatedFinancialPlanQuery request, CancellationToken cancellationToken)
        {
            var user = await _usersRepository.GetUserByIdAsync(new(request.UserId));
            if (user is null)
            {
                return new NotFound<FilledFinancialPlanResult>($"No user with id {request.UserId} found.");
            }

            var financialPlan = await _financialPlansRepository.GetFinancialPlanByIdAsync(new(request.FinancialPlanId));
            if (financialPlan is null)
            {
                return new NotFound<FilledFinancialPlanResult>($"No financial plan with id {request.FinancialPlanId} found.");
            }

            if (financialPlan.GroupId is not null)
            {
                var group = await _groupsRepository.GetGroupByIdAsync(financialPlan.GroupId);
                if (group is null)
                {
                    _logger.LogError("Financial plan {financialPlanId} refers to unexisting group {groupId}", financialPlan.Id, financialPlan.GroupId);
                    return new InternalError<FilledFinancialPlanResult>(message: $"Financial plan with id {financialPlan.Id.Value} refers to unexisting group.");
                }

                var groupUser = group.Users.SingleOrDefault(u => u.UserId == user.Id);
                if (groupUser == default)
                {
                    return new AccessViolation<FilledFinancialPlanResult>($"User with id {user.Id.Value} has no permissions to financial plans in this group.");
                }
            }

            var charges = financialPlan.Charges.Where(c => c.Priority >= request.Priority);
            if (!charges.Any())
            {
                var emptyFilledFinancialPlanResult = new FilledFinancialPlanResult(financialPlan, default, default, []);
                return new SuccessResult<FilledFinancialPlanResult>(emptyFilledFinancialPlanResult);
            }

            if (request.Origin is null)
            {
                _logger.LogInformation("Origin wasn't specified so assumed for now.");
            }

            var calculatedCharges = financialPlan.CalculateFilled(request.Budget, request.Priority, request.Origin ?? DateTime.UtcNow);

            var limitDateOptimistic = calculatedCharges
                .SelectMany(c => c.CalculatedExpirationDates)
                .Where(d => d.Type == CalculatedExpirationDateType.ByMin)
                .Max(d => d.ExpirationDate);

            var liminDatePessimistic = calculatedCharges
                .SelectMany(c => c.CalculatedExpirationDates)
                .Where(d => d.Type == CalculatedExpirationDateType.ByMax)
                .Max(d => d.ExpirationDate);

            var calculatedChargeResults = _mapper.Map<List<CalculatedChargeResult>>(calculatedCharges);

            var result = new FilledFinancialPlanResult(financialPlan, limitDateOptimistic, liminDatePessimistic, calculatedChargeResults);

            return new SuccessResult<FilledFinancialPlanResult>(result);
        }
    }
}
