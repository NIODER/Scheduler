using Scheduler.Domain.Scheduling.ValueObjects;

namespace Scheduler.Domain.Scheduling.Behavior.Actualizing;

internal class DaysScheduleActualizer : IScheduleActualizer
{
    public Schedule Actualize(DateTime origin, Schedule schedule)
    {
        if (schedule.ScheduleType != ScheduleType.Days)
        {
            throw new ArgumentException($"Invalid type of schedule ({schedule.ScheduleType}) for {nameof(DaysScheduleActualizer)}.");
        }

        if (schedule.ScheduledDate <= origin && schedule.Deadline >= origin)
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
