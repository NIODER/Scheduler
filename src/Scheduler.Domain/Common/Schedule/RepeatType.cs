namespace Scheduler.Domain.Common.Schedule;

public enum RepeatType
{
    None = 0,
    Days,
    Weeks,
    Months,
    Years,
    LastDayOfMonth,
    LastDayOfYear
}
