namespace Scheduler.Contracts.Finances;

public record ChargeRequest(
    string ChargeName,
    string Description,
    decimal MaximalCost,
    decimal MinimalCost,
    int Priority,
    int RepeatType,
    DateTimeOffset ExpirationDate,
    DateTime ScheduledDate,
    DateTime Created);
