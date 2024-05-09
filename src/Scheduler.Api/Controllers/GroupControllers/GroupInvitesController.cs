using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Api.Common.Utils;
using Scheduler.Application.GroupInvites.Commands.AcceptGroupInvite;
using Scheduler.Contracts.Groups.GroupInvites;

namespace Scheduler.Api.Controllers.GroupControllers;

[ApiController, Route("group"), Authorize]
public class GroupInvitesController(ISender sender, IMapper mapper) : ControllerBase
{
    private readonly ISender _sender = sender;
    private readonly IMapper _mapper = mapper;

    [HttpPost("{groupId}/invite/{inviteId}")]
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
}
