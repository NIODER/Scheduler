namespace Scheduler.Contracts.Finances;

public record ChargeResponse(
    Guid Id,
    string ChargeName,
    string Description,
    decimal MinimalCost,
    decimal MaximalCost,
    int Priority,
    int RepeatType,
    DateTime ScheduledDate,
    DateTime ExpirationDate,
    DateTime Created);