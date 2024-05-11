using System.Net;

namespace Scheduler.Application.Common.Wrappers;

public class NotFound<T>(string? message = null, Exception? exception = null) : IErrorResult<T>
{
    public HttpStatusCode HttpStatusCode => HttpStatusCode.NotFound;

    public string? ClientMessage => message;

    public Exception? Exception => exception;
}
