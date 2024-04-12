using Scheduler.Domain.Common;

namespace Scheduler.Domain.FinancialPlanAggregate.ValueObjects;

public class Charge : ValueObject
{
    public string ChargeName { get; private set; }
    public string Description { get; private set; }
    public decimal MinimalCost { get; private set; }
    public decimal MaximalCost { get; private set; }
    public int Priority { get; private set; }
    public bool Repeat { get; private set; }
    public uint ExpireDays { get; private set; }
    public DateTime Created { get; private set; }

    private Charge()
    {
        ChargeName = null!;
        Description = null!;
    }

    private Charge(
        string chargeName,
        string description,
        decimal minimalCost,
        decimal maximalCost,
        int priority,
        bool repeat,
        uint expireDays,
        DateTime created
    )
    {
        ChargeName = chargeName;
        Description = description;
        MinimalCost = minimalCost;
        MaximalCost = maximalCost;
        Priority = priority;
        Repeat = repeat;
        ExpireDays = expireDays;
        Created = created;
    }

    public static Charge Create(
        string chargeName,
        string description,
        decimal minimalCost,
        decimal maximalCost,
        int priority,
        bool repeat,
        uint expireDays
    ) => new(chargeName,
        description,
        minimalCost,
        maximalCost,
        priority,
        repeat,
        expireDays,
        DateTime.UtcNow);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return ChargeName;
        yield return Description;
        yield return MinimalCost;
        yield return MaximalCost;
        yield return Priority;
        yield return Repeat;
        yield return ExpireDays;   
        yield return Created;
    }
}
