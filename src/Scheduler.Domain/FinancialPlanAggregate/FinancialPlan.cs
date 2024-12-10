using Scheduler.Domain.Common.DomainDesign;
using Scheduler.Domain.FinancialPlanAggregate.Calculation;
using Scheduler.Domain.FinancialPlanAggregate.Entities;
using Scheduler.Domain.FinancialPlanAggregate.ValueObjects;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Domain.FinancialPlanAggregate;

public class FinancialPlan : Aggregate<FinancialPlanId>
{
    private readonly List<Charge> _charges = [];
    public string Title { get; set; }
    public UserId CreatorId { get; private set; }
    public GroupId? GroupId { get; set; }

    private FinancialPlan()
    {
        Title = null!;
        CreatorId = default!;
        GroupId = default!;
    }

    private FinancialPlan(FinancialPlanId id, string title, UserId creatorId, GroupId? groupId, List<Charge> charges) : base(id)
    {
        Title = title;
        _charges = charges;
        CreatorId = creatorId;
        GroupId = groupId;
    }

    public static FinancialPlan CreatePrivate(
        string title,
        UserId creatorId,
        List<Charge> charges
    ) => new(new FinancialPlanId(Guid.NewGuid()), title, creatorId, null, charges);

    public static FinancialPlan CreateGroup(
        string title,
        UserId creatorId,
        GroupId groupId,
        List<Charge> charges
    ) => new(new FinancialPlanId(Guid.NewGuid()), title, creatorId, groupId, charges);

    public IReadOnlyCollection<Charge> Charges => _charges.AsReadOnly();

    public bool IsPrivate => GroupId is null;

    public List<CalculatedCharge> CalculateFilled(decimal budget, int priority, DateTime origin, bool byMin = true)
    {
        List<Charge> chargesByPriority = Charges.Where(c => c.Priority >= priority).ToList();
        HashSet<CalculatedCharge> chargesBudgetCover = [];
        bool calculated = false;
        int minPeriod = 1; // 1 day for now. Idk how to calculate minimal diff. TODO: Optimize

        do
        {
            foreach (var charge in chargesByPriority)
            {
                if (charge.ActualizeSchedule(origin))
                {
                    continue;
                }

                decimal cost = byMin
                    ? charge.MinimalCost
                    : charge.MaximalCost ?? charge.MinimalCost;

                if (budget - cost <= 0)
                {
                    calculated = true;
                    continue;
                }
                else
                {
                    budget -= cost;
                }

                CalculatedCharge calculatedCharge = new(charge, [charge.Schedule.ScheduledDate]);
                if (chargesBudgetCover.TryGetValue(calculatedCharge, out var listedCalculatedCharge))
                {
                    listedCalculatedCharge.CalculatedExpirationDates.Add(charge.Schedule.ScheduledDate);
                }
                else
                {
                    chargesBudgetCover.Add(calculatedCharge);
                }
            }

            origin = origin.AddDays(minPeriod);
        }
        while (!calculated);

        return [.. chargesBudgetCover];
    }



    public List<CalculatedCharge> CalculateDistributed()
    {
        // Algorythm is:
        // 1. Actualize all charges' schedule in this financial plan
        // 2. For every charge get full count of repeats in this period
        // 3. 
        throw new NotImplementedException();
    }
}