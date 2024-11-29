namespace Scheduler.Domain.Scheduling.ValueObjects;

public enum ScheduleType
{
    None = 0,
    Days,
    Weeks,
    Months,
    Years,
    LastDayOfMonth,
    LastDayOfYear
}
