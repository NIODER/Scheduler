using Scheduler.Domain.FinancialPlanAggregate;

namespace Scheduler.Application.Finances.Common;

public record FilledFinancialPlanResult(
    FinancialPlan FinancialPlan,
    DateTime LimitDateOptimistic,
    DateTime LimitDatePessimistic,
    List<CalculatedChargeResult> Charges);
