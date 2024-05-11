namespace Scheduler.Application.Common.Wrappers;

public interface IErrorResult<T> : ICommandResult<T>
{
    public string? ClientMessage { get; }
    public Exception? Exception { get; }
}
