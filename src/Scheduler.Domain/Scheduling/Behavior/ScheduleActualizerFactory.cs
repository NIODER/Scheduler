using Scheduler.Domain.Scheduling.Behavior.Actualizing;
using Scheduler.Domain.Scheduling.ValueObjects;

namespace Scheduler.Domain.Scheduling.Behavior;

public static class ScheduleActualizerFactory
{
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="NotImplementedException"></exception>
    public static IScheduleActualizer GetActualizer(ScheduleType scheduleType) => scheduleType switch
    {
        ScheduleType.None => throw new InvalidOperationException($"Can't create actualizer for schedule type {ScheduleType.None}"),
        ScheduleType.Days => new DaysScheduleActualizer(),
        ScheduleType.Weeks => new WeeksScheduleActualizer(),
        ScheduleType.Months => new MonthsScheduleActualizer(),
        ScheduleType.Years => throw new NotImplementedException(),
        ScheduleType.LastDayOfMonth => throw new NotImplementedException(),
        ScheduleType.LastDayOfYear => throw new NotImplementedException(),
        _ => throw new NotImplementedException()
    };
}
