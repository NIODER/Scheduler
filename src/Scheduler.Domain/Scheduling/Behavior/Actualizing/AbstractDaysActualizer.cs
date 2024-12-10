using Scheduler.Domain.Scheduling.ValueObjects;

namespace Scheduler.Domain.Scheduling.Behavior.Actualizing;

internal abstract class AbstractDaysActualizer : IScheduleActualizer
{
    public virtual Schedule Actualize(DateTime origin, Schedule schedule)
    {
        if (schedule.IsActual(origin))
        {
            return schedule;
        }

        int daysCount = (schedule.Deadline - schedule.ScheduledDate).Days;
        DateTime newDeadline = schedule.Deadline;
        DateTime newScheduledDate = schedule.ScheduledDate;

        if (newDeadline < origin)
        {
            while (newDeadline < origin)
            {
                newDeadline = newDeadline.AddDays(daysCount);
            }

            newScheduledDate = newDeadline.AddDays(-daysCount);
        }
        else
        {
            while (newScheduledDate > origin)
            {
                newScheduledDate = newScheduledDate.AddDays(-daysCount);
            }

            newDeadline = newScheduledDate.AddDays(daysCount);
        }

        return new Schedule(newScheduledDate, newDeadline, schedule.OriginScheduledDate, schedule.ScheduleType);
    }
}
