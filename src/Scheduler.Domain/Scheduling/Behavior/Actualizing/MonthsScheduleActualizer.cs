using Scheduler.Domain.Scheduling.ValueObjects;

namespace Scheduler.Domain.Scheduling.Behavior.Actualizing;

internal class MonthsScheduleActualizer : IScheduleActualizer
{
    public Schedule Actualize(DateTime origin, Schedule schedule)
    {
        if (schedule.ScheduleType != ScheduleType.Months)
        {
            throw new ArgumentException($"Invalid type of schedule ({schedule.ScheduleType}).");
        }

        if (schedule.IsActual(origin))
        {
            return schedule;
        }

        DateTime newDeadline = schedule.Deadline;
        DateTime newScheduledDate = schedule.ScheduledDate;

        if (origin < newScheduledDate)
        {
            while (origin < newScheduledDate)
            {
                newScheduledDate = newScheduledDate.AddMonths(1);
            }
        }
        else if (origin > newDeadline)
        {
            while (origin > newDeadline)
            {
                newDeadline = newDeadline.AddMonths(-1);
            }
        }

        if (newScheduledDate.Day < schedule.OriginScheduledDate.Day)
        {
            int daysInMonth = DateTime.DaysInMonth(newScheduledDate.Day, newScheduledDate.Month);
            int maxDay = int.Min(daysInMonth, schedule.OriginScheduledDate.Day);
            while (newScheduledDate.Day < maxDay)
            {
                newScheduledDate = newScheduledDate.AddDays(1);
            }
        }

        return new Schedule(newScheduledDate, newDeadline, schedule.OriginScheduledDate, schedule.ScheduleType);
    }
}
