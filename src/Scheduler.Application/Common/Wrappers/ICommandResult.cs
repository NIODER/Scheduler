using System.Net;

namespace Scheduler.Application.Common.Wrappers;

public interface ICommandResult
{
    public HttpStatusCode HttpStatusCode { get; }
}
