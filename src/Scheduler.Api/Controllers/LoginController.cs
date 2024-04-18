using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Application.Authentication.Commands.Login;
using Scheduler.Contracts.Authentication;

namespace Scheduler.Api.Controllers;

[ApiController, Route("[controller]")]
public class LoginController(ISender sender, IMapper mapper) : ControllerBase
{
    private readonly ISender _sender = sender;
    private readonly IMapper _mapper = mapper;

    [HttpPost]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
    {
        var command = _mapper.Map<LoginCommand>(request);
        var result = await _sender.Send(command);
        return Ok(_mapper.Map<AuthenticationResponse>(result));
    }
}