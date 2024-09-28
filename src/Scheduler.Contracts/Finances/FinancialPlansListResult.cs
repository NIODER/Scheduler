namespace Scheduler.Contracts.Finances;

public record FinancialPlansListResult(uint Count, List<FinancialPlanResult> Plans);
