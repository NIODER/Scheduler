using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Application.Common.Wrappers;

namespace Scheduler.Api.Common.Utils;

public static class ResultExtentions
{
    public static IActionResult ActionResult<TResult, TResponse>(this ICommandResult result, IMapper mapper, ILogger logger)
    {
        if (result is ISuccessResult<TResult> successResult)
        {
            return new OkObjectResult(mapper.Map<TResponse>(successResult));
        }
        return result switch
        {
            AccessViolation accessViolationResult => ResultExtentionsHelpers.ProceedForbidResult(accessViolationResult, logger),
            NotFound notFoundResult => ResultExtentionsHelpers.ProceedNotFoundResult(notFoundResult, logger),
            InternalError internalErrorResult => ResultExtentionsHelpers.ProcceedInternalError(internalErrorResult, logger),
            IErrorResult errorResult => ResultExtentionsHelpers.ProceedUnhandledError(errorResult, logger),
            _ => ResultExtentionsHelpers.ProceedUnknownError(logger)
        };
    }
}
