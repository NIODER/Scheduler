namespace Scheduler.Contracts.Finances;

public record FilledFinancialPlanResult(
    Guid FinancialId,
    string Title,
    DateTime LimitDateOptimistic,
    DateTime LimitDatePessimistic,
    List<CalculatedChargeResult> Charges);
