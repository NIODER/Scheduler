using Scheduler.Domain.Scheduling.ValueObjects;

namespace Scheduler.Domain.Tests.DatesActualizerTests;

public class WeeksActualizerTests
{
    [Fact]
    public void OriginAlreadyInPeriodTest()
    {
        DateTime scheduledDate = DateTime.Parse("2001-01-01");
        DateTime expirationDate = DateTime.Parse("2001-01-08");
        DateTime originDate = DateTime.Parse("2001-01-02");

        var schedule = new Schedule(scheduledDate, expirationDate, scheduledDate, ScheduleType.Weeks);
        schedule.Actualize(originDate);

        Assert.Equal(scheduledDate, schedule.OriginScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-01"), schedule.ScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-08"), schedule.Deadline);
    }

    [Fact]
    public void OriginGreaterThanExpirationDateInLimitsOfNextPeriodCorrectsDatesAndReturnsFalseTest()
    {
        DateTime scheduledDate = DateTime.Parse("2001-01-01");
        DateTime expirationDate = DateTime.Parse("2001-01-08");
        DateTime originDate = DateTime.Parse("2001-01-10");

        var schedule = new Schedule(scheduledDate, expirationDate, scheduledDate, ScheduleType.Weeks);
        schedule.Actualize(originDate);

        Assert.Equal(scheduledDate, schedule.OriginScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-08"), schedule.ScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-15"), schedule.Deadline);
    }

    [Fact]
    public void OriginGreaterThanExpirationDateNotInLimitsOfNextPeriodCorrectsDatesAndReturnsFalseTest()
    {
        DateTime scheduledDate = DateTime.Parse("2001-01-01");
        DateTime expirationDate = DateTime.Parse("2001-01-08");
        DateTime originDate = DateTime.Parse("2001-01-17");

        var schedule = new Schedule(scheduledDate, expirationDate, scheduledDate, ScheduleType.Weeks);
        schedule.Actualize(originDate);

        Assert.Equal(scheduledDate, schedule.OriginScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-15"), schedule.ScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-22"), schedule.Deadline);
    }

    [Fact]
    public void OriginLessThanExpirationDateInLimitsOfNextPeriodCorrectsDatesAndReturnsFalseTest()
    {
        DateTime scheduledDate = DateTime.Parse("2001-01-15");
        DateTime expirationDate = DateTime.Parse("2001-01-22");
        DateTime originDate = DateTime.Parse("2001-01-10");

        var schedule = new Schedule(scheduledDate, expirationDate, scheduledDate, ScheduleType.Weeks);
        schedule.Actualize(originDate);

        Assert.Equal(scheduledDate, schedule.OriginScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-08"), schedule.ScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-15"), schedule.Deadline);
    }

    [Fact]
    public void OriginLessThanExpirationDateNotInLimitsOfNextPeriodCorrectsDatesAndReturnsFalseTest()
    {
        DateTime scheduledDate = DateTime.Parse("2001-01-15");
        DateTime expirationDate = DateTime.Parse("2001-01-22");
        DateTime originDate = DateTime.Parse("2001-01-02");

        var schedule = new Schedule(scheduledDate, expirationDate, scheduledDate, ScheduleType.Weeks);
        schedule.Actualize(originDate);

        Assert.Equal(scheduledDate, schedule.OriginScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-01"), schedule.ScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-08"), schedule.Deadline);
    }
}
