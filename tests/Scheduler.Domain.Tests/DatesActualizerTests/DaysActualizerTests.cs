using Scheduler.Domain.FinancialPlanAggregate.Calculation.Dates;

namespace Scheduler.Domain.Tests.DatesActualizerTests;

public class DaysActualizerTests
{
    [Fact]
    public void OriginAlreadyInPeriodReturnsTrueTest()
    {
        DateTime scheduledDate = DateTime.Parse("2001-01-01 10:10:10,000");
        DateTime expirationDate = DateTime.Parse("2001-01-03 10:10:10,000");
        DateTime originDate = DateTime.Parse("2001-01-02 10:10:10,000");

        var actualizer = DatesActualizerFabfic.GetDatesActualizerByRepeatType(Common.Schedule.RepeatType.Days);

        Assert.True(actualizer.ActualizeDates(originDate, ref scheduledDate, ref expirationDate));
        Assert.Equal(DateTime.Parse("2001-01-01 10:10:10,000"), scheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-03 10:10:10,000"), expirationDate);
    }

    [Fact]
    public void OriginGreaterThanExpirationDateInLimitsOfNextPeriodCorrectsDatesAndReturnsFalseTest()
    {
        DateTime scheduledDate = DateTime.Parse("2001-01-01 10:10:10,000");
        DateTime expirationDate = DateTime.Parse("2001-01-03 10:10:10,000");
        DateTime originDate = DateTime.Parse("2001-01-04 10:10:10,000");

        var actualizer = DatesActualizerFabfic.GetDatesActualizerByRepeatType(Common.Schedule.RepeatType.Days);

        Assert.False(actualizer.ActualizeDates(originDate, ref scheduledDate, ref expirationDate));
        Assert.Equal(DateTime.Parse("2001-01-03 10:10:10,000"), scheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-05 10:10:10,000"), expirationDate);
    }

    [Fact]
    public void OriginGreaterThanExpirationDateNotInLimitsOfNextPeriodCorrectsDatesAndReturnsFalseTest()
    {
        DateTime scheduledDate = DateTime.Parse("2001-01-01 10:10:10,000");
        DateTime expirationDate = DateTime.Parse("2001-01-03 10:10:10,000");
        DateTime originDate = DateTime.Parse("2001-01-10 10:10:10,000");

        var actualizer = DatesActualizerFabfic.GetDatesActualizerByRepeatType(Common.Schedule.RepeatType.Days);

        Assert.False(actualizer.ActualizeDates(originDate, ref scheduledDate, ref expirationDate));
        Assert.Equal(DateTime.Parse("2001-01-09 10:10:10,000"), scheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-11 10:10:10,000"), expirationDate);
    }

    [Fact]
    public void OriginLessThanExpirationDateInLimitsOfNextPeriodCorrectsDatesAndReturnsFalseTest()
    {
        DateTime scheduledDate = DateTime.Parse("2001-01-03 10:10:10,000");
        DateTime expirationDate = DateTime.Parse("2001-01-05 10:10:10,000");
        DateTime originDate = DateTime.Parse("2001-01-02 10:10:10,000");

        var actualizer = DatesActualizerFabfic.GetDatesActualizerByRepeatType(Common.Schedule.RepeatType.Days);

        Assert.False(actualizer.ActualizeDates(originDate, ref scheduledDate, ref expirationDate));
        Assert.Equal(DateTime.Parse("2001-01-01 10:10:10,000"), scheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-03 10:10:10,000"), expirationDate);
    }

    [Fact]
    public void OriginLessThanExpirationDateNotInLimitsOfNextPeriodCorrectsDatesAndReturnsFalseTest()
    {
        DateTime scheduledDate = DateTime.Parse("2001-01-9 10:10:10,000");
        DateTime expirationDate = DateTime.Parse("2001-01-11 10:10:10,000");
        DateTime originDate = DateTime.Parse("2001-01-02 10:10:10,000");

        var actualizer = DatesActualizerFabfic.GetDatesActualizerByRepeatType(Common.Schedule.RepeatType.Days);

        Assert.False(actualizer.ActualizeDates(originDate, ref scheduledDate, ref expirationDate));
        Assert.Equal(DateTime.Parse("2001-01-01 10:10:10,000"), scheduledDate);
        Assert.Equal(DateTime.Parse("2001-01-03 10:10:10,000"), expirationDate);
    }
}
