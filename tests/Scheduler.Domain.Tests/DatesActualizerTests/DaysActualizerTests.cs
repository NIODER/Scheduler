using Scheduler.Domain.Scheduling.ValueObjects;

namespace Scheduler.Domain.Tests.DatesActualizerTests;

public class DaysActualizerTests
{
    [Fact]
    public void OriginAlreadyInPeriodTest()
    {
        DateTime scheduledDate = DateTime.Parse("2001-01-01");
        DateTime expirationDate = DateTime.Parse("2001-01-03");
        DateTime originDate = DateTime.Parse("2001-01-02");

        var schedule = new Schedule(scheduledDate, expirationDate, scheduledDate, ScheduleType.Days);
        schedule.Actualize(originDate);

        Assert.Equal(scheduledDate, schedule.OriginScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-01"), schedule.ScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-03"), schedule.Deadline);
    }

    [Fact]
    public void OriginGreaterThanExpirationDateInLimitsOfNextPeriodCorrectsDatesAndReturnsFalseTest()
    {
        DateTime scheduledDate = DateTime.Parse("2001-01-01");
        DateTime expirationDate = DateTime.Parse("2001-01-03");
        DateTime originDate = DateTime.Parse("2001-01-04");

        var schedule = new Schedule(scheduledDate, expirationDate, scheduledDate, ScheduleType.Days);
        schedule.Actualize(originDate);

        Assert.Equal(scheduledDate, schedule.OriginScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-03"), schedule.ScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-05"), schedule.Deadline);
    }

    [Fact]
    public void OriginGreaterThanExpirationDateNotInLimitsOfNextPeriodCorrectsDatesAndReturnsFalseTest()
    {
        DateTime scheduledDate = DateTime.Parse("2001-01-01");
        DateTime expirationDate = DateTime.Parse("2001-01-03");
        DateTime originDate = DateTime.Parse("2001-01-10");

        var schedule = new Schedule(scheduledDate, expirationDate, scheduledDate, ScheduleType.Days);
        schedule.Actualize(originDate);

        Assert.Equal(scheduledDate, schedule.OriginScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-09"), schedule.ScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-11"), schedule.Deadline);
    }

    [Fact]
    public void OriginLessThanExpirationDateInLimitsOfNextPeriodCorrectsDatesAndReturnsFalseTest()
    {
        DateTime scheduledDate = DateTime.Parse("2001-01-03");
        DateTime expirationDate = DateTime.Parse("2001-01-05");
        DateTime originDate = DateTime.Parse("2001-01-02");

        var schedule = new Schedule(scheduledDate, expirationDate, scheduledDate, ScheduleType.Days);
        schedule.Actualize(originDate);

        Assert.Equal(scheduledDate, schedule.OriginScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-01"), schedule.ScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-03"), schedule.Deadline);
    }

    [Fact]
    public void OriginLessThanExpirationDateNotInLimitsOfNextPeriodCorrectsDatesAndReturnsFalseTest()
    {
        DateTime scheduledDate = DateTime.Parse("2001-01-9");
        DateTime expirationDate = DateTime.Parse("2001-01-11");
        DateTime originDate = DateTime.Parse("2001-01-02");

        var schedule = new Schedule(scheduledDate, expirationDate, scheduledDate, ScheduleType.Days);
        schedule.Actualize(originDate);

        Assert.Equal(scheduledDate, schedule.OriginScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-01"), schedule.ScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-03"), schedule.Deadline);
    }
}
