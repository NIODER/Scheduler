using System.Net;

namespace Scheduler.Application.Common.Wrappers;

public class SuccessResult<T>(T? value) : ISuccessResult<T>
{
    public T? Value => value;

    public HttpStatusCode HttpStatusCode => HttpStatusCode.OK;
}
