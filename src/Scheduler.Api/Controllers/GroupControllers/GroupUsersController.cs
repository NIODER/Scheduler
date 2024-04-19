using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Api.Common.Utils;
using Scheduler.Application.Groups.Queries.GetGroupUser;
using Scheduler.Contracts.Groups;

namespace Scheduler.Api.Controllers.GroupControllers;

[ApiController, Route("[controller]/{groupId}/user")]
public class GroupUsersController(ISender sender, IMapper mapper) : ControllerBase
{
    private readonly ISender _sender = sender;
    private readonly IMapper _mapper = mapper;

    [Authorize, HttpGet("{userId}")]
    public async Task<IActionResult> GetGroupUser(Guid groupId, Guid userId)
    {
        var id = HttpContext.GetExecutorUserId();
        if (id is null || userId != id)
        {
            return Forbid();
        }
        var query = new GetGroupUserByGroupAndUserIdQuery(groupId, userId);
        var result = await _sender.Send(query);
        if (result.IsForbidden)
        {
            return Forbid();
        }
        return Ok(_mapper.Map<GroupUserResponse>(result));
    }
}