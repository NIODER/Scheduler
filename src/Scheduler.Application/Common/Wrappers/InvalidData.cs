using System.Net;

namespace Scheduler.Application.Common.Wrappers;

public class InvalidData<T>(string message, Exception? exception = null) : IErrorResult<T>
{
    public string? ClientMessage => $"{message}";

    public Exception? Exception => exception;

    public HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;
}
