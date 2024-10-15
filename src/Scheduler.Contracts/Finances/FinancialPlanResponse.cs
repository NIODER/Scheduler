namespace Scheduler.Contracts.Finances;

public record FinancialPlanResponse(
    Guid FinancialId,
    string Title,
    List<ChargeResponse> Charges);
