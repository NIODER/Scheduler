namespace Scheduler.Contracts.Finances;

public record FinancialPlanRequest(string Title, List<ChargeRequest> Charges);
