namespace Scheduler.Contracts.Common;

public record ResponseError(
    string Message,
    long Code
);