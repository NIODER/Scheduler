using MediatR;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Finances.Common;

namespace Scheduler.Application.Finances.Queries.GetFillCalculatedFinancialPlan;

public record GetFillCalculatedFinancialPlanQuery(Guid UserId, Guid FinancialPlanId, decimal Budget, int Priority, DateTime? Origin)
    : IRequest<ICommandResult<FilledFinancialPlanResult>>;
