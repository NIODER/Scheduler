using Scheduler.Domain.Common.DomainDesign;
using Scheduler.Domain.Common.Schedule;
using Scheduler.Domain.FinancialPlanAggregate.ValueObjects;

namespace Scheduler.Domain.FinancialPlanAggregate.ValueObjects;

public class Charge : Entity<ChargeId> 
{
    public string ChargeName { get; private set; }
    public string Description { get; private set; }
    public decimal MinimalCost { get; private set; }
    public decimal? MaximalCost { get; private set; }
    public int Priority { get; private set; }
    public RepeatType RepeatType { get; private set; }
    public DateTime ScheduledDate { get; private set; }
    public DateTime ExpirationDate { get; private set; }
    public DateTime CreatedDate { get; private set; }

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
        RepeatType repeatType,
        DateTime scheduledDate,
        DateTime expirationDate,
        DateTime createdDate
    ) : base(id)
    {
        ChargeName = chargeName;
        Description = description;
        MinimalCost = minimalCost;
        MaximalCost = maximalCost;
        Priority = priority;
        RepeatType = repeatType;
        ScheduledDate = scheduledDate;
        ExpirationDate = expirationDate;
        CreatedDate = createdDate;
    }

    public static Charge CreateWithRepeat(
        string chargeName,
        string description,
        decimal minimalCost,
        decimal? maximalCost,
        int priority,
        IRepeatDelay repeatDelay,
        DateTime expirationDate
    ) => new(
        id: new(Guid.NewGuid()),
        chargeName,
        description,
        minimalCost: Math.Min(minimalCost, maximalCost ?? decimal.MaxValue),
        maximalCost: Math.Max(minimalCost, maximalCost ?? decimal.MinValue),
        priority,
        repeatDelay.RepeatType,
        repeatDelay.ExpirationDate,
        expirationDate,
        DateTime.UtcNow);
}
