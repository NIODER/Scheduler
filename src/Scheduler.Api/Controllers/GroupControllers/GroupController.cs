using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Api.Common.Utils;
using Scheduler.Application.Groups.Commands.DeleteGroup;
using Scheduler.Application.Groups.Commands.UpdateGroup;
using Scheduler.Application.Groups.Queries;
using Scheduler.Contracts.Groups;

namespace Scheduler.Api.Controllers.GroupControllers;

[ApiController, Route("[controller]")]
public class GroupController(ISender sender, IMapper mapper) : ControllerBase
{
    private readonly ISender _sender = sender;
    private readonly IMapper _mapper = mapper;

    [HttpGet("{groupId}")]
    public async Task<IActionResult> GetGroupGyIdAsync(Guid groupId)
    {
        var command = new GetGroupByIdQuery(groupId);
        var result = await _sender.Send(command);
        return Ok(_mapper.Map<GroupResponse>(result));
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
        var result = await _sender.Send(command);
        if (result.IsForbidden)
        {
            return Forbid();
        }
        return Ok(_mapper.Map<GroupResponse>(result.Result));
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
        var result = await _sender.Send(command);
        if (result.IsForbidden)
        {
            return Forbid();
        }
        return Ok(_mapper.Map<GroupResponse>(result.Result));
    }
}
 