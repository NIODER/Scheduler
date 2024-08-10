using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Api.Common.Utils;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Groups.Commands.CreateGroup;
using Scheduler.Application.Groups.Commands.DeleteGroup;
using Scheduler.Application.Groups.Commands.UpdateGroup;
using Scheduler.Application.Groups.Common;
using Scheduler.Application.Groups.Queries.GetGroup;
using Scheduler.Contracts.Groups;

namespace Scheduler.Api.Controllers.GroupControllers;

[ApiController, Route("[controller]")]
public class GroupController(ISender sender, IMapper mapper, ILogger<GroupController> logger) : ControllerBase
{
    private readonly ISender _sender = sender;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<GroupController> _logger = logger;

    [HttpGet("{groupId}")]
    public async Task<IActionResult> GetGroupGyIdAsync(Guid groupId)
    {
        var command = new GetGroupByIdQuery(groupId);
        ICommandResult<GroupResult> result = await _sender.Send(command);
        return result.ActionResult<GroupResult, GroupResponse>(_mapper, _logger);
    }

    [Authorize, HttpPatch("{groupId}")]
    public async Task<IActionResult> UpdateGroupInformationAsync(Guid groupId, [FromBody]UpdateGroupInformationRequest request)
    {
        var userId = HttpContext.GetExecutorUserId();
        if (userId is null)
        {
            return Forbid();
        }
        var command = new UpdateGroupInformationCommand(groupId, userId.Value, request.GroupName);
        ICommandResult<GroupResult> result = await _sender.Send(command);
        return result.ActionResult<GroupResult, GroupResponse>(_mapper, _logger);
    }

    [Authorize, HttpDelete("{groupId}")]
    public async Task<IActionResult> DeleteGroup(Guid groupId)
    {
        var userId = HttpContext.GetExecutorUserId();
        if (userId is null)
        {
            return Forbid();
        }
        var command = new DeleteGroupCommand(groupId, userId.Value);
        ICommandResult<GroupResult> result = await _sender.Send(command);
        return result.ActionResult<GroupResult, GroupResponse>(_mapper, _logger);
    }

    [Authorize, HttpPost]
    public async Task<IActionResult> CreateGroup([FromBody]CreateGroupRequest request)
    {
        var userId = HttpContext.GetExecutorUserId();
        if (userId is null)
        {
            return Forbid();
        }
        var command = new CreateGroupCommand(userId.Value, request.GroupName);
        ICommandResult<GroupResult> result = await _sender.Send(command);
        return result.ActionResult<GroupResult, GroupResponse>(_mapper, _logger);
    }
}
 