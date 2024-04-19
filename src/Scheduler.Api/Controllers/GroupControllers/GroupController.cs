using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Contracts.Groups;

namespace Scheduler.Api.Controllers.GroupControllers;

[ApiController, Route("[controller]")]
public class GroupController(ISender sender, IMapper mapper) : ControllerBase
{
    private readonly ISender _sender = sender;
    private readonly IMapper _mapper = mapper;


    [HttpGet("{groupId}")]
    public IActionResult GetGroupGyId(Guid groupId)
    {
        throw new NotImplementedException();
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
 