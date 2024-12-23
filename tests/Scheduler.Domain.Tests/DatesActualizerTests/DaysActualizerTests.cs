using Scheduler.Domain.Scheduling.ValueObjects;

namespace Scheduler.Domain.Tests.DatesActualizerTests;

public class DaysActualizerTests
{
    [Fact]
    public void OriginAlreadyInPeriodTest()
    {
        DateTime scheduledDate = DateTime.Parse("2001-01-01");
        DateTime deadlineDate = DateTime.Parse("2001-01-03");
        DateTime originDate = DateTime.Parse("2001-01-02");

        var schedule = Schedule.Create(ScheduleType.Days, scheduledDate, deadlineDate);
        schedule.Actualize(originDate);

        Assert.Equal(scheduledDate, schedule.OriginScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-01"), schedule.ScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-03"), schedule.Deadline);
    }

    [Fact]
    public void OriginGreaterThanDeadlineDateInLimitsOfNextPeriodCorrectsDatesAndReturnsFalseTest()
    {
        DateTime scheduledDate = DateTime.Parse("2001-01-01");
        DateTime deadlineDate = DateTime.Parse("2001-01-03");
        DateTime originDate = DateTime.Parse("2001-01-04");

        var schedule = Schedule.Create(ScheduleType.Days, scheduledDate, deadlineDate);
        schedule.Actualize(originDate);

        Assert.Equal(scheduledDate, schedule.OriginScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-03"), schedule.ScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-05"), schedule.Deadline);
    }

    [Fact]
    public void OriginGreaterThanDeadlineDateNotInLimitsOfNextPeriodCorrectsDatesAndReturnsFalseTest()
    {
        DateTime scheduledDate = DateTime.Parse("2001-01-01");
        DateTime deadlineDate = DateTime.Parse("2001-01-03");
        DateTime originDate = DateTime.Parse("2001-01-10");

        var schedule = Schedule.Create(ScheduleType.Days, scheduledDate, deadlineDate);
        schedule.Actualize(originDate);

        Assert.Equal(scheduledDate, schedule.OriginScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-09"), schedule.ScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-11"), schedule.Deadline);
    }

    [Fact]
    public void OriginLessThanDeadlineDateInLimitsOfNextPeriodCorrectsDatesAndReturnsFalseTest()
    {
        DateTime scheduledDate = DateTime.Parse("2001-01-03");
        DateTime deadlineDate = DateTime.Parse("2001-01-05");
        DateTime originDate = DateTime.Parse("2001-01-02");

        var schedule = Schedule.Create(ScheduleType.Days, scheduledDate, deadlineDate);
        schedule.Actualize(originDate);

        Assert.Equal(scheduledDate, schedule.OriginScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-01"), schedule.ScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-03"), schedule.Deadline);
    }

    [Fact]
    public void OriginLessThanDeadlineDateNotInLimitsOfNextPeriodCorrectsDatesAndReturnsFalseTest()
    {
        DateTime scheduledDate = DateTime.Parse("2001-01-9");
        DateTime deadlineDate = DateTime.Parse("2001-01-11");
        DateTime originDate = DateTime.Parse("2001-01-02");

        var schedule = Schedule.Create(ScheduleType.Days, scheduledDate, deadlineDate);
        schedule.Actualize(originDate);

        Assert.Equal(scheduledDate, schedule.OriginScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-01"), schedule.ScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-03"), schedule.Deadline);
    }
}
