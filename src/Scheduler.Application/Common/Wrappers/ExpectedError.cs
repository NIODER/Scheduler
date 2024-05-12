using System.Net;

namespace Scheduler.Application.Common.Wrappers;

public class ExpectedError<T>(string message) : IErrorResult<T>
{
    public string? ClientMessage => message;

    public Exception? Exception => null;

    public HttpStatusCode HttpStatusCode => HttpStatusCode.Conflict;
}
