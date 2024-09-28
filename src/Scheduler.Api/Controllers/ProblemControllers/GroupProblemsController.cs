using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Api.Common.Utils;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Problems.Common;
using Scheduler.Application.Problems.Queries.GetGroupProblems;
using Scheduler.Contracts.Problems;

namespace Scheduler.Api.Controllers.ProblemControllers;

[ApiController, Route("tasks/group")]
public class GroupProblemsController(ISender sender, IMapper mapper, ILogger<GroupProblemsController> logger) : ControllerBase
{
    private readonly ISender _sender = sender;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<GroupProblemsController> _logger = logger;

    [HttpGet("{groupId}")]
    public async Task<IActionResult> GetGroupProblems(Guid groupId)
    {
        var userId = HttpContext.GetExecutorUserId();
        if (userId is null)
        {
            return Forbid();
        }
        var query = new GetGroupProblemsCommand(groupId, userId.Value);
        ICommandResult<GroupProblemsResult> result = await _sender.Send(query);
        return result.ActionResult<GroupProblemsResult, GroupProblemsResponse>(_mapper, _logger);
    }
}
