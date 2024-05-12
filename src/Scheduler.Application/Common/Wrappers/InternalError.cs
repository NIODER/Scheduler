using System.Net;

namespace Scheduler.Application.Common.Wrappers;

public class InternalError<T>(Exception exception, string? message = null, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) 
    : IErrorResult<T>
{
    public const string DEFAULT_MESSAGE = "Unknown internal error.";

    public HttpStatusCode HttpStatusCode => statusCode;

    public string? ClientMessage => message ?? DEFAULT_MESSAGE;

    public Exception? Exception => exception;
}
