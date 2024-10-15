using Scheduler.Domain.FinancialPlanAggregate.Entities;

namespace Scheduler.Application.Finances.Common;

public record CalculatedChargeResult(
    Charge Charge,
    List<DateTime> ExpirationDates,
    CalculatedChargeStatus Status);
