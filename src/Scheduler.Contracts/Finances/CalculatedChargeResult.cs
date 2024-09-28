namespace Scheduler.Contracts.Finances;

public record CalculatedChargeResult(
    Guid Id,
    string ChargeName,
    string Description,
    decimal MinimalCost,
    decimal MaximalCost,
    int Priority,
    bool Repeat,
    DateTimeOffset Expire,
    DateTime Created,
    string Status);
