namespace Scheduler.Contracts.Finances;

public record CalculatedChargeResponse(
    Guid Id,
    string ChargeName,
    string Description,
    decimal MinimalCost,
    decimal MaximalCost,
    int Priority,
    List<CalculatedChargeExpirationDateResponse> ExpirationDates,
    DateTime Created);
