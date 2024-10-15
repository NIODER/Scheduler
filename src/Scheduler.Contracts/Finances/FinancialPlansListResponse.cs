namespace Scheduler.Contracts.Finances;

public record FinancialPlansListResponse(uint Count, List<FinancialPlanResponse> Plans);
