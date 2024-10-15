using System.Diagnostics;

namespace Scheduler.Domain.Common.Schedule;

public static class RepeatDelayFactory
{
    public static IRepeatDelay GetRepeatDelay(RepeatType repeatType, DateTime scheduledDate, DateTime expirationDate) => repeatType switch
    {
        RepeatType.None => throw new NotImplementedException(),
        RepeatType.Days => throw new NotImplementedException(),
        RepeatType.Weeks => throw new NotImplementedException(),
        RepeatType.Months => throw new NotImplementedException(),
        RepeatType.Years => throw new NotImplementedException(),
        RepeatType.LastDayOfMonth => throw new NotImplementedException(),
        RepeatType.LastDayOfYear => throw new NotImplementedException(),
        _ => throw new UnreachableException()
    };
}
