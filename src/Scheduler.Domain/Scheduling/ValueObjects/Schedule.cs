﻿using Scheduler.Domain.Scheduling.Behavior;

namespace Scheduler.Domain.Scheduling.ValueObjects;

public class Schedule(
    DateTime scheduledDate,
    DateTime deadline,
    DateTime originScheduledDate,
    ScheduleType scheduleType)
{
    public DateTime ScheduledDate { get; private set; } = scheduledDate;
    public DateTime Deadline { get; private set; } = deadline;
    public DateTime OriginScheduledDate { get; private set; } = originScheduledDate;
    public ScheduleType ScheduleType { get; private set; } = scheduleType;

    public static Schedule Create(ScheduleType type, DateTime scheduledDate, DateTime deadline)
        => new(scheduledDate, deadline, scheduledDate, type);

    public bool IsActual(DateTime origin)
        => ScheduledDate <= origin && Deadline >= origin;

    public void Actualize(DateTime origin)
    {
        var scheduleActualizer = ScheduleActualizerFactory.GetActualizer(ScheduleType);
        var actualSchedule = scheduleActualizer.Actualize(origin, this);

        ScheduledDate = actualSchedule.ScheduledDate;
        Deadline = actualSchedule.Deadline;
        OriginScheduledDate = actualSchedule.OriginScheduledDate;
        ScheduleType = actualSchedule.ScheduleType;
    }
<<<<<<< HEAD

    public override bool Equals(object? obj)
        => obj is Schedule schedule && schedule.GetHashCode() == GetHashCode();

    public override int GetHashCode()
        => HashCode.Combine(ScheduledDate, Deadline, OriginScheduledDate, ScheduleType);
=======
>>>>>>> master
}
