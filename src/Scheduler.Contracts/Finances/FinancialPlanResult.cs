namespace Scheduler.Contracts.Finances;

public record FinancialPlanResult(
    Guid FinancialId,
    string Title,
    List<ChargeResult> Charges);
