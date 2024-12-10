using Scheduler.Domain.FinancialPlanAggregate.Entities;

namespace Scheduler.Domain.FinancialPlanAggregate.Calculation;

public class CalculatedCharge(Charge charge, List<DateTime> calculatedExpirationDates)
{
    public Charge Charge { get; init; } = charge;
    public List<DateTime> CalculatedExpirationDates { get; init; } = calculatedExpirationDates;

    private readonly int _hashCode = HashCode.Combine(-125819128, charge.Id);

    public override int GetHashCode()
        => _hashCode;
}
