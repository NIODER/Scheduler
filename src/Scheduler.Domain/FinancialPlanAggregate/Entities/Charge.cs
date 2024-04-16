using Scheduler.Domain.Common;

namespace Scheduler.Domain.FinancialPlanAggregate.ValueObjects;

public class Charge : Entity<ChargeId> 
{
    public string ChargeName { get; private set; }
    public string Description { get; private set; }
    public decimal MinimalCost { get; private set; }
    public decimal? MaximalCost { get; private set; }
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
        ChargeId id,
        string chargeName,
        string description,
        decimal minimalCost,
        decimal maximalCost,
        int priority,
        bool repeat,
        uint expireDays,
        DateTime created
    ) : base(id)
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
        decimal? maximalCost,
        int priority,
        bool repeat,
        uint expireDays
    ) => new(
        id: new(Guid.NewGuid()),
        chargeName,
        description,
        minimalCost: Math.Min(minimalCost, maximalCost ?? decimal.MaxValue),
        maximalCost: Math.Max(minimalCost, maximalCost ?? decimal.MinValue),
        priority,
        repeat,
        expireDays,
        DateTime.UtcNow);
}
