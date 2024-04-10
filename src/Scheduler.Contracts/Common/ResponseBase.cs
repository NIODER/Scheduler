namespace Scheduler.Contracts.Common;

public class ResponseBase(ResponseError responseError, object response)
{
    public DateTime Time { get; init; } = DateTime.UtcNow;
    public ResponseError Error { get; init; } = responseError;
    public object Response { get; init; } = response;
}