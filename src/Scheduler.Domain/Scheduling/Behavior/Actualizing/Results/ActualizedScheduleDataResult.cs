using Scheduler.Domain.Scheduling.ValueObjects;

namespace Scheduler.Domain.Scheduling.Behavior.Actualizing.Results;

internal record ActualizedScheduleDataResult(DateTime NewScheduledDate, DateTime NewDeadlineDate) : IActualizedScheduleData
{
    public DateTime NewScheduledDate { get; init; } = NewScheduledDate;
    public DateTime NewDeadlineDate { get; init; } = NewDeadlineDate;

    public static ActualizedScheduleDataResult CreateFromSchedule(Schedule schedule)
        => new(schedule.ScheduledDate, schedule.Deadline);
}
