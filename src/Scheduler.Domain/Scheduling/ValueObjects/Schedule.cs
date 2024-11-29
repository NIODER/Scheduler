namespace Scheduler.Domain.Scheduling.ValueObjects;

public record Schedule(
    DateTime ScheduledDate,
    DateTime Deadline,
    DateTime OriginScheduledDate,
    ScheduleType ScheduleType);
