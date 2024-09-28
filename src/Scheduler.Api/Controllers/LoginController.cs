using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Api.Common.Utils;
using Scheduler.Application.Authentication.Commands.Login;
using Scheduler.Application.Authentication.Common;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Contracts.Authentication;

namespace Scheduler.Api.Controllers;

[ApiController, Route("[controller]")]
public class LoginController(ISender sender, IMapper mapper, ILogger<LoginController> logger) : ControllerBase
{
    private readonly ISender _sender = sender;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<LoginController> _logger = logger;

    [HttpPost]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
    {
        var command = _mapper.Map<LoginCommand>(request);
        ICommandResult<AuthenticationResult> result = await _sender.Send(command);
        return result.ActionResult<AuthenticationResult, AuthenticationResponse>(_mapper, _logger);
    }
}