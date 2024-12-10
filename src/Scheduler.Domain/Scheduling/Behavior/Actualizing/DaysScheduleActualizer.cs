using Scheduler.Domain.Scheduling.ValueObjects;

namespace Scheduler.Domain.Scheduling.Behavior.Actualizing;

internal class DaysScheduleActualizer : AbstractDaysActualizer
{
    public override Schedule Actualize(DateTime origin, Schedule schedule)
    {
        if (schedule.ScheduleType != ScheduleType.Days)
        {
            throw new ArgumentException($"Invalid type of schedule ({schedule.ScheduleType}) for {nameof(DaysScheduleActualizer)}.");
        }

        return base.Actualize(origin, schedule);
    }
}
