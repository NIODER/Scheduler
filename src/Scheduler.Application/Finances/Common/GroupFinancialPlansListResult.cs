namespace Scheduler.Application.Finances.Common;

public record GroupFinancialPlansListResult(int Count, List<FinancialPlanResult> Plans);
