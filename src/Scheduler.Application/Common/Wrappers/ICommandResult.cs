using System.Net;

namespace Scheduler.Application.Common.Wrappers;

public interface ICommandResult<T>
{
    public HttpStatusCode HttpStatusCode { get; }
}
