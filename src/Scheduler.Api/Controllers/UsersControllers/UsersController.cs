using System.Net;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Api.Common.Utils;
using Scheduler.Application.Users.Commands.UpdateUser;
using Scheduler.Application.Users.Commands.UpdateUserSettings;
using Scheduler.Application.Users.Queries.GetUser;
using Scheduler.Application.Users.Queries.GetUserSettings;
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
            return NotFound();
        }
        return Ok(_mapper.Map<GeneralUserResponse>(user));
    }

    [Authorize, HttpPatch]
    public async Task<IActionResult> UpdateUserByIdAsync([FromBody]UpdateUserRequest request)
    {
        var userId = HttpContext.GetExecutorUserId();
        if (userId is null || userId != request.UserId)
        {
            return Forbid();
        }
        var command = _mapper.Map<UpdateUserCommand>(request);
        var userResult = await _sender.Send(command);
        return Ok(_mapper.Map<GeneralUserResponse>(userResult));
    }

    [Authorize, HttpGet("{userId}/settings")]
    public async Task<IActionResult> GetUserSettings(Guid userId)
    {
        var executorId = HttpContext.GetExecutorUserId();
        if (executorId is null)
        {
            return Forbid();
        }
        var query = new GetUserSettingsQuery(userId, executorId.Value);
        var result = await _sender.Send(query);
        if (result.IsForbidden)
        {
            return Forbid();
        }
        return Ok(_mapper.Map<UserSettingsResponse>(result.Result));
    }

    [Authorize, HttpPost("{userId}/settings")]
    public async Task<IActionResult> UpdateUserSettings(Guid userId, [FromBody]UpdateUserSettingsRequest request)
    {
        var executorId = HttpContext.GetExecutorUserId();
        if (executorId is null)
        {
            return Forbid();
        }
        var command = new UpdateUserSettingsCommand(executorId.Value, userId, request.Settings);
        var result = await _sender.Send(command);
        if (result.IsForbidden)
        {
            return Forbid();
        }
        return Ok(_mapper.Map<UserSettingsResponse>(result.Result));
    }
}