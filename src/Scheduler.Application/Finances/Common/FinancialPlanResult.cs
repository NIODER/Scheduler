namespace Scheduler.Application.Finances.Common;

public record FinancialPlanResult(
    Guid FinancialId,
    string Title,
    List<ChargeResult> Charges);
