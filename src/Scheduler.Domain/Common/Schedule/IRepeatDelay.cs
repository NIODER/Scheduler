namespace Scheduler.Domain.Common.Schedule;

public interface IRepeatDelay
{
    RepeatType RepeatType { get; }
    DateTime ExpirationDate { get; }
    DateTime ScheduledDate { get; }

    void GetNextExpirationDateSince(DateTime origin);
}
