using Scheduler.Domain.Scheduling.Behavior;
using Scheduler.Domain.Scheduling.Behavior.Actualizing;
using Scheduler.Domain.Scheduling.Behavior.Actualizing.Results;

namespace Scheduler.Domain.Scheduling.ValueObjects;

public class Schedule
{
    public DateTime ScheduledDate { get; private set; }
    public DateTime Deadline { get; private set; }
    public DateTime OriginScheduledDate { get; private set; }
    public ScheduleType ScheduleType { get; private set; }

    private Schedule(
        DateTime scheduledDate,
        DateTime deadline,
        DateTime originScheduledDate,
        ScheduleType scheduleType)
    {
        ScheduledDate = scheduledDate;
        Deadline = deadline;
        OriginScheduledDate = originScheduledDate;
        ScheduleType = scheduleType;
    }

    public static Schedule Create(ScheduleType type, DateTime scheduledDate, DateTime deadline)
        => new(scheduledDate, deadline, scheduledDate, type);

    public bool IsActual(DateTime origin)
        => ScheduledDate <= origin && Deadline >= origin;

    public void Actualize(DateTime origin)
    {
        var scheduleActualizer = ScheduleActualizerFactory.GetActualizer(ScheduleType);
        var actualSchedule = scheduleActualizer.Actualize(origin, this);

        SetScheduleData(actualSchedule);
    }

    internal void SetScheduleData(IActualizedScheduleData data)
    {
        if (data is ActualizedScheduleDataResult result)
        {
            ScheduledDate = result.NewScheduledDate;
            Deadline = result.NewDeadlineDate;

            return;
        }

        throw new ArgumentException("Invalid parameter for actualized schedule data.", nameof(data));
    }

    public static bool operator ==(Schedule lhs, Schedule rhs)
        => lhs.Equals(rhs);

    public static bool operator !=(Schedule lhs, Schedule rhs)
        => !lhs.Equals(rhs);

    public override bool Equals(object? obj)
        => obj is Schedule schedule && schedule.GetHashCode() == GetHashCode();

    public override int GetHashCode()
        => HashCode.Combine(ScheduledDate, Deadline, OriginScheduledDate, ScheduleType);
}
