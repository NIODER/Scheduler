namespace Scheduler.Application.Common.Wrappers;

public interface ISuccessResult<T> : ICommandResult
{
    public T Value { get; }
}
