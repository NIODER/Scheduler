namespace Scheduler.Contracts.Finances;

public record ChargeRequest(
    string ChargeName,
    string Description,
    decimal MaximalCost,
    decimal MinimalCost,
    int Priority,
    bool Repeat,
    DateTimeOffset Expire,
    DateTime Created);
