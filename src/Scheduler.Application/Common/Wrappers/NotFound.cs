using System.Net;

namespace Scheduler.Application.Common.Wrappers;

public class NotFound(string? message = null, Exception? exception = null) : IErrorResult
{
    public HttpStatusCode HttpStatusCode => HttpStatusCode.NotFound;

    public string? ClientMessage => message;

    public Exception? Exception => exception;
}
