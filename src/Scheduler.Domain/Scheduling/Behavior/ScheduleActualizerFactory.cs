using Scheduler.Domain.Scheduling.ValueObjects;

namespace Scheduler.Domain.Scheduling.Behavior;

internal static class ScheduleActualizerFactory
{
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="NotImplementedException"></exception>
    public static IScheduleActualizer GetActualizer(ScheduleType scheduleType) => scheduleType switch
    {
        ScheduleType.None => throw new InvalidOperationException($"Can't create actualizer for schedule type {ScheduleType.None}"),
        ScheduleType.Days => throw new NotImplementedException(),
        ScheduleType.Weeks => throw new NotImplementedException(),
        ScheduleType.Months => throw new NotImplementedException(),
        ScheduleType.Years => throw new NotImplementedException(),
        ScheduleType.LastDayOfMonth => throw new NotImplementedException(),
        ScheduleType.LastDayOfYear => throw new NotImplementedException(),
        _ => throw new NotImplementedException()
    };
}
