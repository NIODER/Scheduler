using Microsoft.AspNetCore.Mvc;
using Scheduler.Application.Common.Wrappers;
using System.Diagnostics;
using System.Net;

internal static class ResultExtentionsHelpers
{
    public static IActionResult ProcceedInternalError<T>(InternalError<T> result, ILogger logger)
    {
        logger.LogInformation(result.Exception, message: "Internal error result. Client message: {clientMessage}", result.ClientMessage);
        return new ObjectResult(new ProblemDetails()
        {
            Status = (int)result.HttpStatusCode,
            Title = "Internal server error",
            Detail = result.ClientMessage
        });
    }

    public static IActionResult ProceedForbidResult<T>(AccessViolation<T> result, ILogger logger)
    {
        logger.LogDebug(result.Exception, "Access violation result. Client message: {clientMessage}", result.ClientMessage);
        return new ForbidResult();
    }

    public static IActionResult ProceedNotFoundResult<T>(NotFound<T> result, ILogger logger)
    {
        logger.LogDebug(result.Exception, "Not found result. Client message: {clientMessage}", result.ClientMessage);
        return new NotFoundResult();
    }

    public static IActionResult ProceedUnhandledError<T>(IErrorResult<T> result, ILogger logger)
    {
        logger.LogError(result.Exception, "Unsupported error result. Message: {clientMessage}. Trace: {traceId}", result.ClientMessage, Activity.Current?.Id);
        return new ObjectResult(new ProblemDetails()
        {
            Status = (int)HttpStatusCode.InternalServerError,
            Title = "Unknown error",
            Detail = "Unsupported error occured"
        });
    }

    public static IActionResult ProceedUnknownError(ILogger logger)
    {
        logger.LogError("Unknown error result. Trace: {traceId}", Activity.Current?.Id);
        return new ObjectResult(new ProblemDetails()
        {
            Status = (int)HttpStatusCode.InternalServerError,
            Title = "Unknown error",
            Detail = "Unsupported error occured"
        });
    }
}