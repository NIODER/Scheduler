using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Api.Common.Utils;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Groups.Commands.DeleteGroupUser;
using Scheduler.Application.Groups.Commands.UpdateGroupUser;
using Scheduler.Application.Groups.Common;
using Scheduler.Application.Groups.Queries.GetGroupUser;
using Scheduler.Contracts.Groups;
using Scheduler.Contracts.Groups.GroupUsers;

namespace Scheduler.Api.Controllers.GroupControllers;

[ApiController, Route("group"), Authorize]
public class GroupUsersController(ISender sender, IMapper mapper, ILogger<GroupUsersController> logger) : ControllerBase
{
    private readonly ISender _sender = sender;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<GroupUsersController> _logger = logger;

    [HttpGet("{groupId}/user/{userId}")]
    public async Task<IActionResult> GetGroupUser(Guid groupId, Guid userId)
    {
        var id = HttpContext.GetExecutorUserId();
        if (id is null || userId != id)
        {
            return Forbid();
        }
        var query = new GetGroupUserByGroupAndUserIdQuery(groupId, userId);
        ICommandResult<GroupUserResult> result = await _sender.Send(query);
        return result.ActionResult<GroupUserResult, GroupUserResponse>(_mapper, _logger);
    }

    [HttpPatch("{groupId}/user/{userId}")]
    public async Task<IActionResult> UpdateGroupUser(Guid groupId, Guid userId, [FromBody]UpdateGroupUserRequest request)
    {
        var id = HttpContext.GetExecutorUserId();
        if (id is null)
        {
            return Forbid();
        }
        var command = new UpdateGroupUserCommand(
            GroupId: groupId,
            UserId: userId,
            ExecutorId: id.Value,
            Permissions: request.Permissions
        );
        ICommandResult<GroupUserResult> result = await _sender.Send(command);
        return result.ActionResult<GroupUserResult, GroupUserResponse>(_mapper, _logger);
    }

    [HttpDelete("{groupId}/user/{userId}")]
    public async Task<IActionResult> DeleteGroupUser(Guid groupId, Guid userId)
    {
        var id = HttpContext.GetExecutorUserId();
        if (id is null)
        {
            return Forbid();
        }
        var command = new DeleteGroupUserCommand(
            GroupId: groupId,
            UserId: userId,
            ExecutorId: id.Value
        );
        ICommandResult<GroupUserResult> result = await _sender.Send(command);
        return result.ActionResult<GroupUserResult, GroupUserResponse>(_mapper, _logger);
    }
}