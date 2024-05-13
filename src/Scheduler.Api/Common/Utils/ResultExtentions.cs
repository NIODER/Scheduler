using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Application.Common.Wrappers;

namespace Scheduler.Api.Common.Utils;

public static class ResultExtentions
{
    public static IActionResult ActionResult<TResult, TResponse>(this ICommandResult<TResult> result, IMapper mapper, ILogger logger)
    {
        if (result is ISuccessResult<TResult> successResult)
        {
            return new OkObjectResult(mapper.Map<TResponse>(successResult));
        }
        return result switch
        {
            AccessViolation<TResult> accessViolationResult => ResultExtentionsHelpers.ProceedForbidResult(accessViolationResult, logger),
            NotFound<TResult> notFoundResult => ResultExtentionsHelpers.ProceedNotFoundResult(notFoundResult, logger),
            InternalError<TResult> internalErrorResult => ResultExtentionsHelpers.ProcceedInternalError(internalErrorResult, logger),
            ExpectedError<TResult> expectedErrorResult => ResultExtentionsHelpers.ProceedExpectedErrorResult(expectedErrorResult, logger),
            InvalidData<TResult> invalidDataResult => ResultExtentionsHelpers.ProceeedInvalidDataResult(invalidDataResult, logger),
            IErrorResult<TResult> errorResult => ResultExtentionsHelpers.ProceedUnhandledError(errorResult, logger),
            _ => ResultExtentionsHelpers.ProceedUnknownError(logger)
        };
    }
}
