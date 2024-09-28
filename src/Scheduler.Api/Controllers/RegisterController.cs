using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Api.Common.Utils;
using Scheduler.Application.Authentication.Commands.Registration;
using Scheduler.Application.Authentication.Common;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Contracts.Authentication;

namespace Scheduler.Api.Controllers;

[ApiController, Route("[controller]")]
public class RegisterController(ISender sender, IMapper mapper, ILogger<RegisterController> logger) : ControllerBase
{
    private readonly ISender _sender = sender;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<RegisterController> _logger = logger;

    [HttpPost]
    public async Task<IActionResult> Registrate([FromBody] RegistrateRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);
        ICommandResult<AuthenticationResult> result = await _sender.Send(command);
        return result.ActionResult<AuthenticationResult, AuthenticationResponse>(_mapper, _logger);
    }
}