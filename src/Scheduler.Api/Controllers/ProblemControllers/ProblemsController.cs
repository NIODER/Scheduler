using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Api.Common.Utils;
using Scheduler.Application.Problems.Commands.CreateProblem;
using Scheduler.Application.Problems.Commands.UpdateProblem;
using Scheduler.Application.Problems.Common;
using Scheduler.Application.Problems.Queries.GetProblem;
using Scheduler.Contracts.Problems;

namespace Scheduler.Api.Controllers.ProblemControllers;

[ApiController, Route("task"), Authorize]
public class ProblemsController(IMapper mapper, ISender sender, ILogger<ErrorsController> logger) : ControllerBase
{
    private readonly IMapper _mapper = mapper;
    private readonly ISender _sender = sender;
    private readonly ILogger<ErrorsController> _logger = logger;

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
        var command = new CreateProblemCommand(
            userId.Value,
            request.AssignedUserId,
            request.GroupId,
            request.Title,
            request.Description,
            request.Status,
            request.Deadline);
        var result = await _sender.Send(command);
        return result.ActionResult<ProblemResult, ProblemResponse>(_mapper, _logger);
    }

    [HttpPatch("{taskId}")]
    public async Task<IActionResult> UpdateTask(Guid taskId, [FromBody] ProblemRequest request)
    {
        var userId = HttpContext.GetExecutorUserId();
        if (userId is null)
        {
            return Forbid();
        }
        var command = new UpdateProblemCommand(
            taskId,
            userId.Value,
            request.AssignedUserId,
            request.Title,
            request.Description,
            request.Status,
            request.Deadline);
        var result = await _sender.Send(command);
        return result.ActionResult<ProblemResult, ProblemResponse>(_mapper, _logger);
    }
}
