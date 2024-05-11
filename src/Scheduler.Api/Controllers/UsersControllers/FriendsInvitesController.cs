using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Api.Common.Utils;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.FriendsInvites.Commands.AcceptFriendsInvite;
using Scheduler.Application.FriendsInvites.Commands.CreateFriendsInvite;
using Scheduler.Application.FriendsInvites.Commands.DeleteFriendsInvite;
using Scheduler.Application.FriendsInvites.Common;
using Scheduler.Application.FriendsInvites.Queries.GetFriendsInvite;
using Scheduler.Contracts.Users.UserInvites;

namespace Scheduler.Api.Controllers.UsersControllers;

[ApiController, Authorize, Route("invite")]
public sealed class FriendsInvitesController(ISender sender, IMapper mapper, ILogger logger) : ControllerBase
{
    private readonly ISender _sender = sender;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger _logger = logger;

    [HttpGet("user/{inviteId}")]
    public async Task<IActionResult> GetUserInvite(Guid inviteId)
    {
        var id = HttpContext.GetExecutorUserId();
        if (id is null)
        {
            return Forbid();
        }
        var query = new GetFriendsInviteQuery(inviteId, id.Value);
        ICommandResult<FriendsInviteResult> result = await _sender.Send(query);
        return result.ActionResult<FriendsInviteResult, UserInvitesResponse>(_mapper, _logger);
    }

    [HttpPost("user/{inviteId}")]
    public async Task<IActionResult> AcceptUserInvite(Guid inviteId)
    {
        var id = HttpContext.GetExecutorUserId();
        if (id is null)
        {
            return Forbid();
        }
        var command = new AcceptFriendsInviteCommand(inviteId, id.Value);
        ICommandResult<FriendsInviteResult> result = await _sender.Send(command);
        return result.ActionResult<FriendsInviteResult, UserInvitesResponse>(_mapper, _logger);
    }

    [HttpPut("user/{addressieId}")]
    public async Task<IActionResult> CreateInvite(Guid addressieId, [FromBody] CreateUserInviteRequest request)
    {
        var senderId = HttpContext.GetExecutorUserId();
        if (senderId is null)
        {
            return Forbid();
        }
        var command = new CreateFriendsInviteCommand(
            SenderId: senderId.Value,
            AddressieId: addressieId,
            request.Message
        );
        ICommandResult<FriendsInviteResult> result = await _sender.Send(command);
        return result.ActionResult<FriendsInviteResult, UserInvitesResponse>(_mapper, _logger);
    }

    [HttpDelete("user/{inviteId}")]
    public async Task<IActionResult> DeleteInvite(Guid inviteId)
    {
        var id = HttpContext.GetExecutorUserId();
        if (id is null)
        {
            return Forbid();
        }
        var command = new DeleteFriendsInviteCommand(SenderId: id.Value, InviteId: inviteId);
        ICommandResult<FriendsInviteResult> result = await _sender.Send(command);
        return result.ActionResult<FriendsInviteResult, UserInvitesResponse>(_mapper, _logger);
    }
}