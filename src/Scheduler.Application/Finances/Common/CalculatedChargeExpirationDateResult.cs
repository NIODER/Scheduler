using Scheduler.Domain.FinancialPlanAggregate.Calculation;

namespace Scheduler.Application.Finances.Common;

public record CalculatedChargeExpirationDateResult(DateTime ExpirationDate, CalculatedExpirationDateType Type);
