namespace Scheduler.Contracts.Finances;

public record DistributedFinancialPlanResponse(Guid FinancialId, string Title, List<CalculatedChargeResponse> Charges);
