namespace Scheduler.Application.Common.Wrappers;

public interface IErrorResult : ICommandResult
{
    public string? ClientMessage { get; }
    public Exception? Exception { get; }
}
