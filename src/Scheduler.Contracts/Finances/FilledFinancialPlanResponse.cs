namespace Scheduler.Contracts.Finances;

public record FilledFinancialPlanResponse(
    Guid FinancialId,
    string Title,
    DateTime LimitDateOptimistic,
    DateTime LimitDatePessimistic,
    List<CalculatedChargeResponse> Charges);
