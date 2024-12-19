using Scheduler.Domain.FinancialPlanAggregate;
<<<<<<< HEAD
using Scheduler.Domain.FinancialPlanAggregate.Calculation;
=======
>>>>>>> master
using Scheduler.Domain.FinancialPlanAggregate.Entities;
using Scheduler.Domain.Scheduling.ValueObjects;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Domain.Tests.FinancialPlanTests;

public class CalculateFilledFinancialPlanTests
{
<<<<<<< HEAD
    // TODO: write tests for filled fp
    [Fact]
    public void CalculateFilledFinancialPlanTest()
    {
        var charge1 = Charge.CreateWithRepeat(
                chargeName: "Charge1_Week",
                description: string.Empty,
                minimalCost: 1000,
                maximalCost: null,
                priority: 1,
                schedule: Schedule.Create(ScheduleType.Weeks, DateTime.Parse("2000-01-01"), DateTime.Parse("2000-01-08")));

        var charge2 = Charge.CreateWithRepeat(
                chargeName: "Charge2_Days",
                description: string.Empty,
                minimalCost: 1000,
                maximalCost: null,
                priority: 1,
                schedule: Schedule.Create(ScheduleType.Days, DateTime.Parse("2000-01-01"), DateTime.Parse("2000-01-05")));

        var charge3 = Charge.CreateWithRepeat(
                chargeName: "Charge3_Month",
                description: string.Empty,
                minimalCost: 1000,
                maximalCost: null,
                priority: 1,
                schedule: Schedule.Create(ScheduleType.Months, DateTime.Parse("2000-01-01"), DateTime.Parse("2000-02-01")));

        List<Charge> charges = [charge1, charge2, charge3];

        var financialPlan = FinancialPlan.CreatePrivate("FinancialPlan1", new UserId(Guid.NewGuid()), charges);
        var budget = 14000;

        var expectedCalculatedCharges = new List<CalculatedCharge>()
        {
            new(charge1, [ // every sat
                DateTime.Parse("2000-01-08"),
                DateTime.Parse("2000-01-15"),
                DateTime.Parse("2000-01-22"),
                DateTime.Parse("2000-01-29"),
                DateTime.Parse("2000-02-05")
            ]),
            new(charge2, [ // every 3 days (3 days between)
                DateTime.Parse("2000-01-05"),
                DateTime.Parse("2000-01-09"),
                DateTime.Parse("2000-01-13"),
                DateTime.Parse("2000-01-17"),
                DateTime.Parse("2000-01-21"),
                DateTime.Parse("2000-01-25"),
                DateTime.Parse("2000-01-29"),
                DateTime.Parse("2000-02-02")
            ]),
            new(charge3, [ // every month
                DateTime.Parse("2000-02-01")
            ])
        };

        var realCalculatedCharged = financialPlan.CalculateFilled(budget, 1, DateTime.Parse("2000-01-01"));

        Assert.Equal(expectedCalculatedCharges, realCalculatedCharged);
=======
    [Fact]
    public void CalculateFilledFinancialPlanTest()
    {
        List<Charge> charges = [
            Charge.CreateWithRepeat("Charge1_Month", string.Empty, 1000, null, 1, Schedule.Create(ScheduleType.Months, DateTime.Parse(""), DateTime.Parse(""))),
            Charge.CreateWithRepeat("Charge2_Week", string.Empty, 100, 1000, 1, Schedule.Create(ScheduleType.Months, DateTime.Parse(""), DateTime.Parse(""))),
            Charge.CreateWithRepeat("Charge3_Days", string.Empty, 100, 1000, 1, Schedule.Create(ScheduleType.Months, DateTime.Parse(""), DateTime.Parse(""))),
            ];
        var financialPlan = FinancialPlan.CreatePrivate("FinancialPlan1", new UserId(Guid.NewGuid()), charges);
        var budget = 1;

        financialPlan.CalculateFilled(budget, 1, DateTime.Parse(""));

        throw new NotImplementedException();
>>>>>>> master
    }
}
