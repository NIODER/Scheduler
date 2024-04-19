using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Application.Users.Commands.UpdateUser;
using Scheduler.Application.Users.Queries.GetUser;
using Scheduler.Contracts.Users;

namespace Scheduler.Api.Controllers;

[ApiController, Route("[controller]")]
public class UserController(ISender sender, IMapper mapper) : ControllerBase
{
    private readonly ISender _sender = sender;
    private readonly IMapper _mapper = mapper;

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserById(Guid userId)
    {
        var command = new GetUserByIdQuery(userId);
        var user = await _sender.Send(command);
        if (user is null)
        {
            return Problem(statusCode: (int)HttpStatusCode.NotFound, title: $"No user with id {userId} found.");
        }
        return Ok(_mapper.Map<GeneralUserResponse>(user));
    }

    [Authorize, HttpPatch]
    public async Task<IActionResult> UpdateUserByIdAsync([FromBody]UpdateUserRequest request)
    {
        var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
        var id = claimsIdentity?.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        if (!Guid.TryParse(id, out Guid userId) || userId != request.UserId)
        {
            return Problem(statusCode: (int)HttpStatusCode.Forbidden);
        }
        var command = _mapper.Map<UpdateUserCommand>(request);
        var userResult = await _sender.Send(command);
        return Ok(_mapper.Map<GeneralUserResponse>(userResult));
    }
}