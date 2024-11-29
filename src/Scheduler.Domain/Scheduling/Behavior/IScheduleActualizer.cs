using Scheduler.Domain.Scheduling.ValueObjects;

namespace Scheduler.Domain.Scheduling.Behavior;

public interface IScheduleActualizer
{
    /// <summary>
    /// Actualizing schedule dates by origin. So origin will be between <see cref="Schedule.ScheduledDate"/> and <see cref="Schedule.Deadline"/>
    /// </summary>
    /// <param name="origin">Date origin that should be target for actualizing</param>
    /// <param name="schedule">Schedule need to actuzlize</param>
    /// <returns>Actual schedule</returns>
    /// <exception cref="ArgumentException"></exception>
    public Schedule Actualize(DateTime origin, Schedule schedule);
}
