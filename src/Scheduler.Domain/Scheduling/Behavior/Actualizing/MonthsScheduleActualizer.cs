using Scheduler.Domain.Scheduling.Behavior.Actualizing.Results;
using Scheduler.Domain.Scheduling.ValueObjects;

namespace Scheduler.Domain.Scheduling.Behavior.Actualizing;

internal class MonthsScheduleActualizer : IScheduleActualizer
{
    public IActualizedScheduleData Actualize(DateTime origin, Schedule schedule)
    {
        if (schedule.ScheduleType != ScheduleType.Months)
        {
            throw new ArgumentException($"Invalid type of schedule ({schedule.ScheduleType}).");
        }

        if (schedule.IsActual(origin))
        {
            return ActualizedScheduleDataResult.CreateFromSchedule(schedule);
        }

        int deadlineDay = GetDeadlineDay(origin, schedule);

        if (origin.Day < deadlineDay)
        {
            return GetScheduleWithScheduledDateInPreviousMonth(origin, schedule);
        }
        else if (origin.Day > deadlineDay)
        {
            return GetScheduleWithScheduledDateInCurrentMonth(origin, schedule, deadlineDay);
        }
        else
        {
            return new ActualizedScheduleDataResult(
                NewScheduledDate: origin,
                NewDeadlineDate: origin.AddMonths(1));
        }
    }

    private static ActualizedScheduleDataResult GetScheduleWithScheduledDateInCurrentMonth(DateTime origin, Schedule schedule, int deadlineDay)
    {
        var newScheduledDate = new DateTime(
            year: origin.Year,
            month: origin.Month,
            day: deadlineDay);

        var nextDeadlineDateWithInvalidDay = origin.AddMonths(1);
        var newDeadlineDate = new DateTime(
            year: nextDeadlineDateWithInvalidDay.Year,
            month: nextDeadlineDateWithInvalidDay.Month,
            day: GetDeadlineDay(nextDeadlineDateWithInvalidDay, schedule));

        return new ActualizedScheduleDataResult(
            NewScheduledDate: newScheduledDate,
            NewDeadlineDate: newDeadlineDate);
    }

    private static ActualizedScheduleDataResult GetScheduleWithScheduledDateInPreviousMonth(DateTime origin, Schedule schedule)
    {
        var nextScheduledDateWithInvaildDay = origin.AddMonths(-1);
        var newScheduledDate = new DateTime(
            year: nextScheduledDateWithInvaildDay.Year,
            month: nextScheduledDateWithInvaildDay.Month,
            day: GetDeadlineDay(nextScheduledDateWithInvaildDay, schedule));

        var newDeadlineDate = new DateTime(
            year: origin.Year,
            month: origin.Month,
            day: GetDeadlineDay(origin, schedule));

        return new ActualizedScheduleDataResult(
            NewScheduledDate: newScheduledDate,
            NewDeadlineDate: newDeadlineDate);
    }

    /// <summary>
    /// Returns max day in month for deadline
    /// </summary>
    private static int GetDeadlineDay(DateTime origin, Schedule schedule)
    {
        int deadlineDay = DateTime.DaysInMonth(origin.Year, origin.Month);

        if (deadlineDay > schedule.OriginScheduledDate.Day)
        {
            deadlineDay = schedule.OriginScheduledDate.Day;
        }

        return deadlineDay;
    }
}
