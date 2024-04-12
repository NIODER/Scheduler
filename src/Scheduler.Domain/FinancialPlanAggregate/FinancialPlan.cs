using Scheduler.Domain.Common;
using Scheduler.Domain.FinancialPlanAggregate.ValueObjects;

namespace Scheduler.Domain.FinancialPlanAggregate;

public class FinancialPlan : Aggregate<FinancialPlanId>
{
    private readonly List<Charge> _charges = [];
    public string Title { get; private set; }

    private FinancialPlan()
    {
        Title = null!;
    }

    private FinancialPlan(FinancialPlanId id, string title, List<Charge> charges) : base(id)
    {
        Title = title;
        _charges = charges;
    }

    public static FinancialPlan Create(
        string title,
        List<Charge> charges
    ) => new(FinancialPlanId.CreateUnique(), title, charges);

    public IReadOnlyCollection<Charge> Charges => _charges.AsReadOnly();
}