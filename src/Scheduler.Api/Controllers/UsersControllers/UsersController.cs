using System.Net;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Api.Common.Utils;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Users.Commands.UpdateUser;
using Scheduler.Application.Users.Commands.UpdateUserSettings;
using Scheduler.Application.Users.Common;
using Scheduler.Application.Users.Queries.GetUser;
using Scheduler.Application.Users.Queries.GetUserSettings;
using Scheduler.Contracts.Users;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Scheduler.Api.Controllers;

[ApiController, Route("[controller]")]
public class UserController(ISender sender, IMapper mapper, ILogger logger) : ControllerBase
{
    private readonly ISender _sender = sender;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger _logger = logger;

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserById(Guid userId)
    {
        var command = new GetUserByIdQuery(userId);
        ICommandResult<UserResult>? result = await _sender.Send(command);
        return result.ActionResult<UserResult, GeneralUserResponse>(_mapper, _logger);
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
        ICommandResult<UserResult> result = await _sender.Send(command);
        return result.ActionResult<UserResult, GeneralUserResponse>(_mapper, _logger);
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
        ICommandResult<UserSettingsResult> result = await _sender.Send(query);
        return result.ActionResult<UserSettingsResult, UserSettingsResponse>(_mapper, _logger);
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
        ICommandResult<UserSettingsResult> result = await _sender.Send(command);
        return result.ActionResult<UserSettingsResult, UserSettingsResponse>(_mapper, _logger);
    }
}