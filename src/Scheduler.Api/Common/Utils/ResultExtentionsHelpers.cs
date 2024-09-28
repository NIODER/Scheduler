using Microsoft.AspNetCore.Mvc;
using Scheduler.Application.Common.Wrappers;
using System.Diagnostics;
using System.Net;

internal static class ResultExtentionsHelpers
{
    internal static IActionResult ProcceedInternalError<T>(InternalError<T> result, ILogger logger)
    {
        logger.LogInformation(result.Exception, message: "Internal error result. Client message: {clientMessage}", result.ClientMessage);
        return new ObjectResult(new ProblemDetails()
        {
            Status = (int)result.HttpStatusCode,
            Title = "Internal server error",
            Detail = result.ClientMessage
        });
    }

    internal static IActionResult ProceedForbidResult<T>(AccessViolation<T> result, ILogger logger)
    {
        logger.LogDebug(result.Exception, "Access violation result. Client message: {clientMessage}", result.ClientMessage);
        return new ForbidResult();
    }

    internal static IActionResult ProceedNotFoundResult<T>(NotFound<T> result, ILogger logger)
    {
        logger.LogDebug(result.Exception, "Not found result. Client message: {clientMessage}", result.ClientMessage);
        return new NotFoundResult();
    }

    internal static IActionResult ProceedUnhandledError<T>(IErrorResult<T> result, ILogger logger)
    {
        logger.LogError(result.Exception, "Unsupported error result. Message: {clientMessage}. Trace: {traceId}", result.ClientMessage, Activity.Current?.Id);
        return new ObjectResult(new ProblemDetails()
        {
            Status = (int)HttpStatusCode.InternalServerError,
            Title = "Unknown error",
            Detail = "Unsupported error occured"
        });
    }

    internal static IActionResult ProceedUnknownError(ILogger logger)
    {
        logger.LogError("Unknown error result. Trace: {traceId}", Activity.Current?.Id);
        return new ObjectResult(new ProblemDetails()
        {
            Status = (int)HttpStatusCode.InternalServerError,
            Title = "Unknown error",
            Detail = "Unsupported error occured"
        });
    }

    internal static IActionResult ProceedExpectedErrorResult<TResult>(ExpectedError<TResult> result, ILogger logger)
    {
        logger.LogDebug(result.Exception, "Expected error result. Client message: {clientMessage}", result.ClientMessage);
        return new ObjectResult(new ProblemDetails()
        {
            Status = (int)result.HttpStatusCode,
            Title = "Conflict",
            Detail = result.ClientMessage
        });
    }

    internal static IActionResult ProceeedInvalidDataResult<TResult>(InvalidData<TResult> result, ILogger logger)
    {
        logger.LogDebug(result.Exception, "Invalid data result. Client message: {clientMessage}", result.ClientMessage);
        return new ObjectResult(new ProblemDetails()
        {
            Status = (int)result.HttpStatusCode,
            Title = "Invalid request data",
            Detail = result.ClientMessage
        });
    }
}