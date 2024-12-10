using Scheduler.Domain.FinancialPlanAggregate;
using Scheduler.Domain.FinancialPlanAggregate.Entities;
using Scheduler.Domain.Scheduling.ValueObjects;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Domain.Tests.FinancialPlanTests;

public class CalculateFilledFinancialPlanTests
{
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
    }
}
