using Scheduler.Domain.FinancialPlanAggregate.Entities;

namespace Scheduler.Domain.FinancialPlanAggregate.Calculation;

public class CalculatedCharge(Charge charge, List<DateTime> calculatedExpirationDates)
{
    public Charge Charge { get; init; } = charge;
    public List<DateTime> CalculatedExpirationDates { get; init; } = calculatedExpirationDates;

    private readonly int _hashCode = HashCode.Combine(charge.Id);

    public static bool operator ==(CalculatedCharge calculatedCharge1, CalculatedCharge calculatedCharge2)
        => calculatedCharge1.Equals(calculatedCharge2);

    public static bool operator !=(CalculatedCharge calculatedCharge1, CalculatedCharge calculatedCharge2)
        => !calculatedCharge1.Equals(calculatedCharge2);

    public override bool Equals(object? obj)
        => obj is CalculatedCharge calculatedCharge && calculatedCharge.GetHashCode() == GetHashCode();

    public override int GetHashCode()
        => _hashCode;
}
