using Scheduler.Domain.Common.DomainDesign;
using Scheduler.Domain.FinancialPlanAggregate.ValueObjects;
using Scheduler.Domain.Scheduling.Behavior;
using Scheduler.Domain.Scheduling.ValueObjects;

namespace Scheduler.Domain.FinancialPlanAggregate.Entities;

public class Charge : Entity<ChargeId>
{
    public string ChargeName { get; private set; }
    public string Description { get; private set; }
    public decimal MinimalCost { get; private set; }
    public decimal? MaximalCost { get; private set; }
    public int Priority { get; private set; }
    public Schedule Schedule { get; private set; }
    public DateTime CreatedAt { get; private set; }

    [Obsolete("Do not use. Only for entity framework")]
    private Charge()
    {
        ChargeName = null!;
        Description = null!;
        Schedule = null!;
    }

    protected Charge(
        ChargeId id,
        string chargeName,
        string description,
        decimal minimalCost,
        decimal maximalCost,
        int priority,
        Schedule schedule,
        DateTime createdAt
    ) : base(id)
    {
        ChargeName = chargeName;
        Description = description;
        MinimalCost = minimalCost;
        MaximalCost = maximalCost;
        Priority = priority;
        Schedule = schedule;
        CreatedAt = createdAt;
    }

    public static Charge CreateWithRepeat(
        string chargeName,
        string description,
        decimal minimalCost,
        decimal? maximalCost,
        int priority,
        Schedule schedule
    ) => new(
        id: new(Guid.NewGuid()),
        chargeName,
        description,
        minimalCost: Math.Min(minimalCost, maximalCost ?? decimal.MaxValue),
        maximalCost: Math.Max(minimalCost, maximalCost ?? decimal.MinValue),
        priority,
        schedule,
        DateTime.UtcNow);

    /// <returns><see langword="true"/> if already was actual</returns>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="NotImplementedException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public bool ActualizeSchedule(DateTime origin)
    {
        if (Schedule.IsActual(origin))
        {
            return true;
        }

        var scheduleActualizer = ScheduleActualizerFactory.GetActualizer(Schedule.ScheduleType);
        Schedule = scheduleActualizer.Actualize(origin, Schedule);

        return false;
    }
}
