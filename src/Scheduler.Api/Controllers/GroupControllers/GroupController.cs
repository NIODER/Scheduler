using System.Net;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Api.Common.Utils;
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

    [HttpPatch("{groupId}")]
    public async Task<IActionResult> UpdateGroupInformationAsync(Guid groupId, [FromBody]UpdateGroupInformationRequest request)
    {
        var userId = HttpContext.GetExecutorUserId();
        if (userId is null)
        {
            return Problem(statusCode: (int)HttpStatusCode.Forbidden);
        }
        var command = new UpdateGroupInformationCommand(groupId, userId.Value, request.GroupName);
        var result = await _sender.Send(command);
        if (result.IsForbidden)
        {
            return Problem(statusCode: (int)HttpStatusCode.Forbidden, detail: result.ClientMessage);
        }
        return Ok(_mapper.Map<GroupResponse>(result.Result));
    }

    [HttpDelete("{groupId}")]
    public IActionResult DeleteGroup(Guid groupId)
    {
        throw new NotImplementedException();
    }
}
 