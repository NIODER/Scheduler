using Scheduler.Domain.Scheduling.ValueObjects;

namespace Scheduler.Domain.Tests.DatesActualizerTests;

public class WeeksActualizerTests
{
    [Fact]
    public void OriginAlreadyInPeriodTest()
    {
        DateTime scheduledDate = DateTime.Parse("2001-01-01");
        DateTime deadlineDate = DateTime.Parse("2001-01-08");
        DateTime originDate = DateTime.Parse("2001-01-02");

        var schedule = Schedule.Create(ScheduleType.Weeks, scheduledDate, deadlineDate);
        schedule.Actualize(originDate);

        Assert.Equal(scheduledDate, schedule.OriginScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-01"), schedule.ScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-08"), schedule.Deadline);
    }

    [Fact]
    public void OriginGreaterThanDeadlineDateInLimitsOfNextPeriodCorrectsDatesAndReturnsFalseTest()
    {
        DateTime scheduledDate = DateTime.Parse("2001-01-01");
        DateTime deadlineDate = DateTime.Parse("2001-01-08");
        DateTime originDate = DateTime.Parse("2001-01-10");

        var schedule = Schedule.Create(ScheduleType.Weeks, scheduledDate, deadlineDate);
        schedule.Actualize(originDate);

        Assert.Equal(scheduledDate, schedule.OriginScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-08"), schedule.ScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-15"), schedule.Deadline);
    }

    [Fact]
    public void OriginGreaterThanDeadlineDateNotInLimitsOfNextPeriodCorrectsDatesAndReturnsFalseTest()
    {
        DateTime scheduledDate = DateTime.Parse("2001-01-01");
        DateTime deadlineDate = DateTime.Parse("2001-01-08");
        DateTime originDate = DateTime.Parse("2001-01-17");

        var schedule = Schedule.Create(ScheduleType.Weeks, scheduledDate, deadlineDate);
        schedule.Actualize(originDate);

        Assert.Equal(scheduledDate, schedule.OriginScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-15"), schedule.ScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-22"), schedule.Deadline);
    }

    [Fact]
    public void OriginLessThanDeadlineDateInLimitsOfNextPeriodCorrectsDatesAndReturnsFalseTest()
    {
        DateTime scheduledDate = DateTime.Parse("2001-01-15");
        DateTime deadlineDate = DateTime.Parse("2001-01-22");
        DateTime originDate = DateTime.Parse("2001-01-10");

        var schedule = Schedule.Create(ScheduleType.Weeks, scheduledDate, deadlineDate);
        schedule.Actualize(originDate);

        Assert.Equal(scheduledDate, schedule.OriginScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-08"), schedule.ScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-15"), schedule.Deadline);
    }

    [Fact]
    public void OriginLessThanDeadlineDateNotInLimitsOfNextPeriodCorrectsDatesAndReturnsFalseTest()
    {
        DateTime scheduledDate = DateTime.Parse("2001-01-15");
        DateTime deadlineDate = DateTime.Parse("2001-01-22");
        DateTime originDate = DateTime.Parse("2001-01-02");

        var schedule = Schedule.Create(ScheduleType.Weeks, scheduledDate, deadlineDate);
        schedule.Actualize(originDate);

        Assert.Equal(scheduledDate, schedule.OriginScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-01"), schedule.ScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-08"), schedule.Deadline);
    }
}
