namespace Scheduler.Contracts.Finances;

public record ChargeResult(
    Guid Id,
    string ChargeName,
    string Description,
    decimal MinimalCost,
    decimal MaximalCost,
    int Priority,
    bool Repeat,
    DateTimeOffset Expire,
    DateTime Created);
