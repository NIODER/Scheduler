using Scheduler.Domain.Scheduling.ValueObjects;

namespace Scheduler.Domain.Tests.DatesActualizerTests;

public class MonthsActualizerTests
{
    [Fact]
    public void OriginAlreadyInPeriodTest()
    {
        DateTime scheduledDate = DateTime.Parse("2001-01-01");
        DateTime expirationDate = DateTime.Parse("2001-02-01");
        DateTime originDate = DateTime.Parse("2001-01-02");

        var schedule = new Schedule(scheduledDate, expirationDate, scheduledDate, ScheduleType.Months);
        schedule.Actualize(originDate);

        Assert.Equal(scheduledDate, schedule.OriginScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-01"), schedule.ScheduledDate);
        Assert.Equal(DateTime.Parse("2001-02-01"), schedule.Deadline);
    }

    [Fact]
    public void OriginGreaterThanExpirationDateInLimitsOfNextPeriodCorrectsDatesAndReturnsFalseTest()
    {
        DateTime scheduledDate = DateTime.Parse("2001-01-01");
        DateTime expirationDate = DateTime.Parse("2001-02-01");
        DateTime originDate = DateTime.Parse("2001-02-02");

        var schedule = new Schedule(scheduledDate, expirationDate, scheduledDate, ScheduleType.Months);
        schedule.Actualize(originDate);

        Assert.Equal(scheduledDate, schedule.OriginScheduledDate);
        Assert.Equal(DateTime.Parse("2001-02-01"), schedule.ScheduledDate);
        Assert.Equal(DateTime.Parse("2001-03-01"), schedule.Deadline);
    }

    [Fact]
    public void OriginGreaterThanExpirationDateNotInLimitsOfNextPeriodCorrectsDatesAndReturnsFalseTest()
    {
        DateTime scheduledDate = DateTime.Parse("2001-01-01");
        DateTime expirationDate = DateTime.Parse("2001-02-01");
        DateTime originDate = DateTime.Parse("2001-04-02");

        var schedule = new Schedule(scheduledDate, expirationDate, scheduledDate, ScheduleType.Months);
        schedule.Actualize(originDate);

        Assert.Equal(scheduledDate, schedule.OriginScheduledDate);
        Assert.Equal(DateTime.Parse("2001-04-01"), schedule.ScheduledDate);
        Assert.Equal(DateTime.Parse("2001-05-01"), schedule.Deadline);
    }

    [Fact]
    public void OriginLessThanExpirationDateInLimitsOfNextPeriodCorrectsDatesAndReturnsFalseTest()
    {
        DateTime scheduledDate = DateTime.Parse("2001-02-01");
        DateTime expirationDate = DateTime.Parse("2001-03-01");
        DateTime originDate = DateTime.Parse("2001-01-02");

        var schedule = new Schedule(scheduledDate, expirationDate, scheduledDate, ScheduleType.Months);
        schedule.Actualize(originDate);

        Assert.Equal(scheduledDate, schedule.OriginScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-01"), schedule.ScheduledDate);
        Assert.Equal(DateTime.Parse("2001-02-01"), schedule.Deadline);
    }

    [Fact]
    public void OriginLessThanExpirationDateNotInLimitsOfNextPeriodCorrectsDatesAndReturnsFalseTest()
    {
        DateTime scheduledDate = DateTime.Parse("2001-04-01");
        DateTime expirationDate = DateTime.Parse("2001-05-01");
        DateTime originDate = DateTime.Parse("2001-01-02");

        var schedule = new Schedule(scheduledDate, expirationDate, scheduledDate, ScheduleType.Months);
        schedule.Actualize(originDate);

        Assert.Equal(scheduledDate, schedule.OriginScheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-01"), schedule.ScheduledDate);
        Assert.Equal(DateTime.Parse("2001-02-01"), schedule.Deadline);
    }

    [Fact]
    public void OriginScheduledDateIsGreaterThanDaysCountInNextMonthTest()
    {
        DateTime scheduledDate = DateTime.Parse("1999-12-31");
        DateTime expirationDate = DateTime.Parse("2000-01-31");
        DateTime originDate = DateTime.Parse("2000-02-01");

        var schedule = new Schedule(scheduledDate, expirationDate, scheduledDate, ScheduleType.Months);
        schedule.Actualize(originDate);

        Assert.Equal(scheduledDate, schedule.OriginScheduledDate);
        Assert.Equal(DateTime.Parse("2000-01-31"), schedule.ScheduledDate);
        Assert.Equal(DateTime.Parse("2000-02-29"), schedule.Deadline);
    }

    [Fact]
    public void OriginScheduledDayIsLessThanDaysCountInNextMonthTest()
    {
        DateTime scheduledDate = DateTime.Parse("2000-01-29");
        DateTime expirationDate = DateTime.Parse("2000-02-29");
        DateTime originDate = DateTime.Parse("2000-03-01");

        var schedule = new Schedule(scheduledDate, expirationDate, scheduledDate, ScheduleType.Months);
        schedule.Actualize(originDate);

        Assert.Equal(scheduledDate, schedule.OriginScheduledDate);
        Assert.Equal(DateTime.Parse("2000-02-29"), schedule.ScheduledDate);
        Assert.Equal(DateTime.Parse("2000-03-29"), schedule.Deadline);
    }
}
