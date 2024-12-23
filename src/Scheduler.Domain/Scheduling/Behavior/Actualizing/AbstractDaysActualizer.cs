using Scheduler.Domain.Scheduling.Behavior.Actualizing.Results;
using Scheduler.Domain.Scheduling.ValueObjects;

namespace Scheduler.Domain.Scheduling.Behavior.Actualizing;

internal abstract class AbstractDaysActualizer : IScheduleActualizer
{
    public virtual IActualizedScheduleData Actualize(DateTime origin, Schedule schedule)
    {
        if (schedule.IsActual(origin))
        {
            return ActualizedScheduleDataResult.CreateFromSchedule(schedule);
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

        return new ActualizedScheduleDataResult(
            NewScheduledDate: newScheduledDate,
            NewDeadlineDate: newDeadline);
    }
}
