using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Scheduler.Api.Controllers;

public class ErrorsController(ILogger<ErrorsController> logger) : ControllerBase
{
    private readonly ILogger<ErrorsController> _logger = logger;

    [ApiExplorerSettings(IgnoreApi = true), Route("/error")]
    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        if (exception is ValidationException ex)
        {
            var modelStateDictionary = new ModelStateDictionary();
            foreach (var error in ex.Errors)
            {
                modelStateDictionary.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            return ValidationProblem(modelStateDictionary);
        }
        _logger.LogError(exception, "Exception catched in errors controller. Message: {message}.", exception?.Message);
        return Problem(statusCode: (int)HttpStatusCode.InternalServerError, title: exception?.Message);
    }
}