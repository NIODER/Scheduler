using System.Net;

namespace Scheduler.Application.Common.Wrappers;

public class AccessViolation<T>(string? message = null, Exception? exception = null) : IErrorResult<T>
{
    public HttpStatusCode HttpStatusCode => HttpStatusCode.Forbidden;

    public string? ClientMessage => message;

    public Exception? Exception => exception;
}
