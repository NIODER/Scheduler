using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Api.Common.Utils;
using Scheduler.Application.GroupInvites.Commands.AcceptGroupInvite;
using Scheduler.Application.GroupInvites.Commands.CreateGroupInvite;
using Scheduler.Application.GroupInvites.Commands.DeleteGroupInvite;
using Scheduler.Application.Groups.Commands.CreateGroup;
using Scheduler.Contracts.Groups.GroupInvites;
using System.Diagnostics.CodeAnalysis;

namespace Scheduler.Api.Controllers.GroupControllers;

[ApiController, Route("group"), Authorize]
public class GroupInvitesController(ISender sender, IMapper mapper) : ControllerBase
{
    private readonly ISender _sender = sender;
    private readonly IMapper _mapper = mapper;

    [HttpPost("{groupId}/invite")]
    public async Task<IActionResult> AcceptInvite(Guid groupId, Guid inviteId)
    {
        var userId = HttpContext.GetExecutorUserId();
        if (userId is null)
        {
            return Forbid();
        }
        var command = new AcceptGroupInviteCommand(
            groupId,
            userId.Value,
            inviteId);
        var result = await _sender.Send(command);
        return Ok(_mapper.Map<GroupInviteResponse>(result));
    }

    [HttpPut("{groupId}/invite")]
    public async Task<IActionResult> CreateGroupInvite(Guid groupId, [FromBody] GroupInviteRequest request)
    {
        var userId = HttpContext.GetExecutorUserId();
        if (userId is null)
        {
            return Forbid();
        }
        var command = new CreateGroupInviteCommand(
            groupId,
            userId.Value,
            request.Expire,
            request.Usages,
            request.Permissions,
            request.Message);
        var result = await _sender.Send(command);
        if (result.IsForbidden)
        {
            return Forbid();
        }
        return Ok(_mapper.Map<GroupInviteResponse>(result.Result));
    }

    [HttpDelete("{groupId}/invite/{inviteId}")]
    public async Task<IActionResult> DeleteGroupInvite(Guid groupId, Guid inviteId)
    {
        var userId = HttpContext.GetExecutorUserId();
        if (userId is null)
        {
            return Forbid();
        }
        var command = new DeleteGroupInviteCommand(
            userId.Value,
            groupId,
            inviteId);
        var result = await _sender.Send(command);
        if (result.IsForbidden)
        {
            return Forbid();
        }
        return Ok(_mapper.Map<GroupInviteResponse>(result.Result));
    }
}
