using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
    public IActionResult UpdateGroupInformation([FromBody]UpdateGroupInformationRequest request)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{groupId}")]
    public IActionResult DeleteGroup(Guid groupId)
    {
        throw new NotImplementedException();
    }
}
 