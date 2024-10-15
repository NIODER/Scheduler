namespace Scheduler.Application.Finances.Common;

public record UserFinancialPlansListResult(
    int Count,
    List<FinancialPlanResult> Plans);
