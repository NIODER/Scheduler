using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Application.Authentication.Commands.Registration;
using Scheduler.Contracts.Authentication;

namespace Scheduler.Api.Controllers;

[ApiController, Route("[controller]")]
public class RegisterController(ISender sender, IMapper mapper) : ControllerBase
{
    private readonly ISender _sender = sender;
    private readonly IMapper _mapper = mapper;

    [HttpPost]
    public async Task<IActionResult> Registrate([FromBody] RegistrateRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);
        var result = await _sender.Send(command);
        return Ok(_mapper.Map<AuthenticationResponse>(result));
    }
}