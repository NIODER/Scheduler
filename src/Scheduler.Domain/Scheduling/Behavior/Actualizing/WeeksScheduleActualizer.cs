using Scheduler.Domain.Scheduling.ValueObjects;

namespace Scheduler.Domain.Scheduling.Behavior.Actualizing;

internal class WeeksScheduleActualizer : AbstractDaysActualizer
{
    public override Schedule Actualize(DateTime origin, Schedule schedule)
    {
        if (schedule.ScheduleType != ScheduleType.Weeks)
        {
            throw new ArgumentException($"Invalid type of schedule ({schedule.ScheduleType}) for {nameof(WeeksScheduleActualizer)}.");
        }

        int daysCount = (schedule.Deadline - schedule.ScheduledDate).Days;

        if (daysCount % 7 != 0)
        {
            throw new InvalidOperationException($"Invalid delay for weeks. It must be multiple of days in week.");
        }

        return base.Actualize(origin, schedule);
    }
}
