using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Api.Common.Utils;
using Scheduler.Application.Problems.Common;
using Scheduler.Application.Problems.Queries.GetProblem;
using Scheduler.Contracts.Problems;

namespace Scheduler.Api.Controllers.ProblemControllers;

[ApiController, Route("task"), Authorize]
public class ProblemsController(IMapper mapper, ISender sender, ILogger logger) : ControllerBase
{
    private readonly IMapper _mapper = mapper;
    private readonly ISender _sender = sender;
    private readonly ILogger _logger = logger;

    [HttpGet("{taskId}")]
    public async Task<IActionResult> GetTask(Guid taskId)
    {
        var userId = HttpContext.GetExecutorUserId();
        if (userId is null)
        {
            return Forbid();
        }
        var query = new GetProblemByIdQuery(userId.Value, taskId);
        var result = await _sender.Send(query);
        return result.ActionResult<ProblemResult, ProblemResponse>(_mapper, _logger);
    }

    [HttpPut]
    public async Task<IActionResult> CreateTask([FromBody] ProblemRequest request)
    {
        var userId = HttpContext.GetExecutorUserId();
        if (userId is null)
        {
            return Forbid();
        }
    }
}
