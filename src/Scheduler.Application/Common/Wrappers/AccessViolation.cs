using System.Net;

namespace Scheduler.Application.Common.Wrappers;

public class AccessViolation(string? message = null, Exception? exception = null) : IErrorResult
{
    public HttpStatusCode HttpStatusCode => HttpStatusCode.Forbidden;

    public string? ClientMessage => message;

    public Exception? Exception => exception;
}
