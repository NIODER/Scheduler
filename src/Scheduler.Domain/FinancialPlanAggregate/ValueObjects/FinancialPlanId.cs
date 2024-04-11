using Scheduler.Domain.Common;

namespace Scheduler.Domain.FinancialPlanAggregate.ValueObjects;

public sealed class FinancialPlanId : ValueObject
{
    public Guid Id { get; init; }

    private FinancialPlanId(Guid id)
    {
        Id = id;
    }

    public static FinancialPlanId CreateUnique()
    {
        return new(Guid.NewGuid());

    }
    public static FinancialPlanId Create(Guid id)
    {
        return new(id);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }
}