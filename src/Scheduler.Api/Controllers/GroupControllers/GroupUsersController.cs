using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Api.Common.Utils;
using Scheduler.Application.Groups.Commands.DeleteGroupUser;
using Scheduler.Application.Groups.Commands.UpdateGroupUser;
using Scheduler.Application.Groups.Queries.GetGroupUser;
using Scheduler.Contracts.Groups;
using Scheduler.Contracts.Groups.GroupUsers;

namespace Scheduler.Api.Controllers.GroupControllers;

[ApiController, Route("group"), Authorize]
public class GroupUsersController(ISender sender, IMapper mapper) : ControllerBase
{
    private readonly ISender _sender = sender;
    private readonly IMapper _mapper = mapper;

    [HttpGet("{groupId}/user/{userId}")]
    public async Task<IActionResult> GetGroupUser(Guid groupId, Guid userId)
    {
        var id = HttpContext.GetExecutorUserId();
        if (id is null || userId != id)
        {
            return Forbid();
        }
        var query = new GetGroupUserByGroupAndUserIdQuery(groupId, userId);
        var result = await _sender.Send(query);
        if (result.IsForbidden)
        {
            return Forbid();
        }
        return Ok(_mapper.Map<GroupUserResponse>(result.Result));
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
        var result = await _sender.Send(command);
        if (result.IsForbidden)
        {
            return Forbid();
        }
        return Ok(_mapper.Map<GroupUserResponse>(result.Result));
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
        var result = await _sender.Send(command);
        if (result.IsForbidden)
        {
            return Forbid();
        }
        return Ok(_mapper.Map<GroupUserResponse>(result.Result));
    }
}