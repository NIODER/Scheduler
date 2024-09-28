namespace Scheduler.Contracts.Finances;

public record DistributedFinancialPlanResutl(Guid FinancialId, string Title, List<CalculatedChargeResult> Charges);
