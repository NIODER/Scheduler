using Scheduler.Domain.Common.DomainDesign;
using Scheduler.Domain.FinancialPlanAggregate.Calculation;
using Scheduler.Domain.FinancialPlanAggregate.Entities;
using Scheduler.Domain.FinancialPlanAggregate.ValueObjects;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate.ValueObjects;
using System.Diagnostics;

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

    public List<CalculatedCharge> CalculateFilled(decimal budget, int priority, DateTime origin)
    {
        List<Charge> chargesByPriority = Charges.Where(c => c.Priority >= priority).ToList();
        HashSet<CalculatedCharge> chargesBudgetCover = [];
        bool minCalculated = false;
        bool maxCalculated = false;
        int minPeriod = 1;
        decimal budgetByMin = budget;
        decimal budgetByMax = budget;

        do
        {
            foreach (var charge in chargesByPriority)
            {
                if (charge.ActualizeSchedule(origin))
                {
                    continue;
                }

                if (!minCalculated)
                {
                    decimal costByMin = charge.MinimalCost;

                    if (budgetByMin - costByMin < 0)
                    {
                        minCalculated = true;

                        if (maxCalculated)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        budgetByMin -= costByMin;
                    }
                }

                if (!maxCalculated)
                {
                    decimal costByMax = charge.MaximalCost ?? charge.MinimalCost;

                    if (budgetByMax - costByMax < 0)
                    {
                        maxCalculated = true;

                        if (minCalculated)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        budgetByMax -= costByMax;
                    }
                }

                CalculatedExpirationDateType type = (minCalculated, maxCalculated) switch
                {
                    (false, false) => CalculatedExpirationDateType.ByBoth,
                    (false, true) => CalculatedExpirationDateType.ByMax,
                    (true, false) => CalculatedExpirationDateType.ByMin,
                    _ => throw new UnreachableException("Program logic error, min and max are calculated but trying to continue.")
                };

                CalculatedExpirationDate calculatedExpirationDate = new(charge.Schedule.ScheduledDate, type);

                CalculatedCharge calculatedCharge = new(charge, [calculatedExpirationDate]);

                if (chargesBudgetCover.TryGetValue(calculatedCharge, out var listedCalculatedCharge))
                {
                    listedCalculatedCharge.CalculatedExpirationDates.Add(calculatedExpirationDate);
                }
                else
                {
                    chargesBudgetCover.Add(calculatedCharge);
                }
            }

            origin = origin.AddDays(minPeriod);
        }
        while (!maxCalculated || !minCalculated);

        var calculatedChargesList = chargesBudgetCover.ToList();
        SortInOriginOrder(calculatedChargesList);

        return calculatedChargesList;
    }

    public List<CalculatedCharge> CalculateDistributed(decimal budget, DateTime periodEnd)
    {
        throw new NotImplementedException();
    }

    private void SortInOriginOrder(List<CalculatedCharge> calculatedCharges)
    {
        calculatedCharges.Sort((c1, c2) =>
        {
            var index1 = _charges.IndexOf(c1.Charge);
            var index2 = _charges.IndexOf(c2.Charge);

            if (index1 == -1)
            {
                throw new InvalidDataException($"Charges of financial plan {Id.Value} is not contains chosen calculated charge {c1.Charge.Id}");
            }
            if (index2 == -1)
            {
                throw new InvalidDataException($"Charges of financial plan {Id.Value} is not contains chosen calculated charge {c2.Charge.Id}");
            }

            return index1.CompareTo(index2);
        });
    }
}