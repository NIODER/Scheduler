namespace Scheduler.Application.Common.Wrappers;

public interface ISuccessResult<T> : ICommandResult<T>
{
    public T? Value { get; }
}
