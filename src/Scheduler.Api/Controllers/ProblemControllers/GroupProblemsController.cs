using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Api.Common.Utils;
using Scheduler.Application.Problems.Queries.GetGroupProblems;
using Scheduler.Contracts.Problems;

namespace Scheduler.Api.Controllers.ProblemControllers;

[ApiController, Route("tasks/group")]
public class GroupProblemsController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public GroupProblemsController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpGet("{groupId}")]
    public async Task<IActionResult> GetGroupProblems(Guid groupId)
    {
        var userId = HttpContext.GetExecutorUserId();
        if (userId is null)
        {
            return Forbid();
        }
        var query = new GetGroupProblemsCommand(groupId, userId.Value);
        var result = await _sender.Send(query);
        if (result.IsForbidden)
        {
            return Forbid();
        }
        return Ok(_mapper.Map<GroupProblemsResponse>(result.Result));
    }
}
