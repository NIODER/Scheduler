using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Api.Common.Utils;
using Scheduler.Application.Users.Commands.AcceptUserInvite;
using Scheduler.Application.Users.Commands.CreateUserInvite;
using Scheduler.Application.Users.Commands.DeleteUserInvite;
using Scheduler.Application.Users.Queries.GetUserInvite;
using Scheduler.Contracts.Invites.UserInvites;

namespace Scheduler.Api.Controllers;

[ApiController, Authorize, Route("invite")]
public sealed class InvitesController(ISender sender, IMapper mapper) : ControllerBase
{
    private readonly ISender _sender = sender;
    private readonly IMapper _mapper = mapper;

    [HttpGet("user/{inviteId}")]
    public async Task<IActionResult> GetUserInvite(Guid inviteId)
    {
        var id = HttpContext.GetExecutorUserId();
        if (id is null)
        {
            return Forbid();
        }
        var query = new GetUserInviteQuery(inviteId, id.Value);
        var result = await _sender.Send(query);
        return Ok(_mapper.Map<UserInvitesResponse>(result));
    }

    [HttpPost("user/{inviteId}")]
    public async Task<IActionResult> AcceptUserInvite(Guid inviteId)
    {
        var id = HttpContext.GetExecutorUserId();
        if (id is null)
        {
            return Forbid();
        }
        var command = new AcceptUserInviteCommand(inviteId, id.Value);
        var result = await _sender.Send(command);
        return Ok(_mapper.Map<UserInvitesResponse>(result));
    }

    [HttpPut("user/{addressieId}")]
    public async Task<IActionResult> CreateInvite(Guid addressieId, [FromBody]CreateUserInviteRequest request)
    {
        var senderId = HttpContext.GetExecutorUserId();
        if (senderId is null)
        {
            return Forbid();
        }
        var command = new CreateUserInviteCommand(
            SenderId: senderId.Value,
            AddressieId: addressieId,
            request.Message
        );
        var result = await _sender.Send(command);
        return Ok(_mapper.Map<UserInvitesResponse>(result));
    }

    [HttpDelete("{inviteId}")]
    public async Task<IActionResult> DeleteInvite(Guid inviteId)
    {
        var id = HttpContext.GetExecutorUserId();
        if (id is null)
        {
            return Forbid();
        }
        var command = new DeleteUserInviteCommand(SenderId: id.Value, InviteId: inviteId);
        var result = await _sender.Send(command);
        return Ok(_mapper.Map<UserInvitesResponse>(result));
    }
}