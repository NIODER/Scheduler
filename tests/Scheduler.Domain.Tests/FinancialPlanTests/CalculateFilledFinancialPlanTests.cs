using Scheduler.Domain.FinancialPlanAggregate;
using Scheduler.Domain.FinancialPlanAggregate.Calculation;
using Scheduler.Domain.FinancialPlanAggregate.Entities;
using Scheduler.Domain.Scheduling.ValueObjects;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Domain.Tests.FinancialPlanTests;

public class CalculateFilledFinancialPlanTests
{
    [Fact]
    public void CalculateFilledFinancialPlanBudgetEnoughExactlyForBothTest()
    {
        var charge1 = Charge.CreateWithRepeat(
                chargeName: "Charge1_Week",
                description: string.Empty,
                minimalCost: 1000,
                priority: 1,
                schedule: Schedule.Create(ScheduleType.Weeks, DateTime.Parse("2000-01-01"), DateTime.Parse("2000-01-08")));

        var charge2 = Charge.CreateWithRepeat(
                chargeName: "Charge2_Days",
                description: string.Empty,
                minimalCost: 1000,
                priority: 1,
                schedule: Schedule.Create(ScheduleType.Days, DateTime.Parse("2000-01-01"), DateTime.Parse("2000-01-05")));

        var charge3 = Charge.CreateWithRepeat(
                chargeName: "Charge3_Month",
                description: string.Empty,
                minimalCost: 1000,
                priority: 1,
                schedule: Schedule.Create(ScheduleType.Months, DateTime.Parse("2000-01-01"), DateTime.Parse("2000-02-01")));

        List<Charge> charges = [charge1, charge2, charge3];

        var financialPlan = FinancialPlan.CreatePrivate("FinancialPlan1", new UserId(Guid.NewGuid()), charges);
        var budget = 14000;

        var expectedCalculatedCharges = new List<CalculatedCharge>()
        {
            new(charge1, [ // every sat
                new(DateTime.Parse("2000-01-08"), CalculatedExpirationDateType.ByBoth),
                new(DateTime.Parse("2000-01-15"), CalculatedExpirationDateType.ByBoth),
                new(DateTime.Parse("2000-01-22"), CalculatedExpirationDateType.ByBoth),
                new(DateTime.Parse("2000-01-29"), CalculatedExpirationDateType.ByBoth),
                new(DateTime.Parse("2000-02-05"), CalculatedExpirationDateType.ByBoth)
            ]),
            new(charge2, [ // every 3 days (3 days between)
                new(DateTime.Parse("2000-01-05"), CalculatedExpirationDateType.ByBoth),
                new(DateTime.Parse("2000-01-09"), CalculatedExpirationDateType.ByBoth),
                new(DateTime.Parse("2000-01-13"), CalculatedExpirationDateType.ByBoth),
                new(DateTime.Parse("2000-01-17"), CalculatedExpirationDateType.ByBoth),
                new(DateTime.Parse("2000-01-21"), CalculatedExpirationDateType.ByBoth),
                new(DateTime.Parse("2000-01-25"), CalculatedExpirationDateType.ByBoth),
                new(DateTime.Parse("2000-01-29"), CalculatedExpirationDateType.ByBoth),
                new(DateTime.Parse("2000-02-02"), CalculatedExpirationDateType.ByBoth)
            ]),
            new(charge3, [ // every month
                new(DateTime.Parse("2000-02-01"), CalculatedExpirationDateType.ByBoth)
            ])
        };

        var realCalculatedCharged = financialPlan.CalculateFilled(budget, 1, DateTime.Parse("2000-01-01"));

        Assert.Equal(expectedCalculatedCharges, realCalculatedCharged);
    }

    [Fact]
    public void CalculateFilledWithZeroBudgetTest()
    {
        var charge1 = Charge.CreateWithRepeat(
                chargeName: "Charge1_Week",
                description: string.Empty,
                minimalCost: 1000,
                priority: 1,
                schedule: Schedule.Create(ScheduleType.Weeks, DateTime.Parse("2000-01-01"), DateTime.Parse("2000-01-08")));

        var charge2 = Charge.CreateWithRepeat(
                chargeName: "Charge2_Days",
                description: string.Empty,
                minimalCost: 1000,
                priority: 1,
                schedule: Schedule.Create(ScheduleType.Days, DateTime.Parse("2000-01-01"), DateTime.Parse("2000-01-05")));

        var charge3 = Charge.CreateWithRepeat(
                chargeName: "Charge3_Month",
                description: string.Empty,
                minimalCost: 1000,
                priority: 1,
                schedule: Schedule.Create(ScheduleType.Months, DateTime.Parse("2000-01-01"), DateTime.Parse("2000-02-01")));

        List<Charge> charges = [charge1, charge2, charge3];
        var financialPlan = FinancialPlan.CreatePrivate("FinancialPlan1", new UserId(Guid.NewGuid()), charges);
        var budget = 0;
        var expectedCalculatedCharges = new List<CalculatedCharge>();

        var readCalculatedCharges = financialPlan.CalculateFilled(budget, 1, DateTime.Parse("2000-01-01"));

        Assert.Equal(expectedCalculatedCharges, readCalculatedCharges);
    }

    [Fact]
    public void CalculateFilledForTwoYearsByBoth()
    {
        DateTime scheduled = DateTime.Parse("1999-01-01");
        DateTime deadline = DateTime.Parse("1999-01-05");

        var charge1 = Charge.CreateWithRepeat("Charge1_Days", string.Empty, 1000, 1, Schedule.Create(ScheduleType.Days, scheduled, deadline));
        var charge2 = Charge.CreateWithRepeat("Charge2_Month", string.Empty, 1000, 1, Schedule.Create(ScheduleType.Months, scheduled, scheduled.AddMonths(1)));
        List<Charge> charges = [charge1, charge2];

        const int CHARGES_IN_TWO_YEARS = 182;

        decimal budget = 216000;

        DateTime origin = scheduled;

        var financialPlan = FinancialPlan.CreatePrivate("fin", new UserId(Guid.NewGuid()), charges);

        List<CalculatedCharge> expected = [new CalculatedCharge(charge1, []), new CalculatedCharge(charge2, [])];

        for (int i = 0; i < CHARGES_IN_TWO_YEARS; i++)
        {
            expected[0].CalculatedExpirationDates.Add(new CalculatedExpirationDate(origin, CalculatedExpirationDateType.ByBoth));
            origin.AddDays(4);
        }

        origin = scheduled;

        for (int i = 0; i < 24; i++)
        {
            expected[1].CalculatedExpirationDates.Add(new CalculatedExpirationDate(origin, CalculatedExpirationDateType.ByBoth));
        }

        var real = financialPlan.CalculateFilled(budget, 1, scheduled);

        Assert.Equal(expected, real);
    }

    // TODO: write tests for two years with min and max
    // need to recalculate for minimal with same budget
}
