using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Api.Common.Utils;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Groups.Commands.AcceptGroupInvite;
using Scheduler.Application.Groups.Commands.CreateGroupInvite;
using Scheduler.Application.Groups.Commands.DeleteGroupInvite;
using Scheduler.Application.Groups.Common;
using Scheduler.Contracts.Groups.GroupInvites;

namespace Scheduler.Api.Controllers.GroupControllers;

[ApiController, Route("group"), Authorize]
public class GroupInvitesController(ISender sender, IMapper mapper, ILogger logger) : ControllerBase
{
    private readonly ISender _sender = sender;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger _logger = logger;

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
        ICommandResult<GroupInviteResult> result = await _sender.Send(command);
        return result.ActionResult<GroupInviteResult, GroupInviteResponse>(_mapper, _logger);
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
        ICommandResult<GroupInviteResult> result = await _sender.Send(command);
        return result.ActionResult<GroupInviteResult, GroupInviteResponse>(_mapper, _logger);
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
        ICommandResult<GroupInviteResult> result = await _sender.Send(command);
        return result.ActionResult<GroupInviteResult, GroupInviteResponse>(_mapper, _logger);
    }
}
