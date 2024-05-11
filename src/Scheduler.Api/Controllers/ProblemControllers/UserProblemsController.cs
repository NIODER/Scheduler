using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Api.Common.Utils;
using Scheduler.Application.Problems.Queries.GetUserProblems;
using Scheduler.Contracts.Problems;

namespace Scheduler.Api.Controllers.ProblemControllers;

[ApiController, Route("tasks/user"), Authorize]
public class UserProblemsController(ISender sender, IMapper mapper) : ControllerBase
{
    private readonly ISender _sender = sender;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<IActionResult> GetUserTasks()
    {
        var userId = HttpContext.GetExecutorUserId();
        if (userId is null)
        {
            return Forbid();
        }
        var query = new GetUserProblemsQuery(userId.Value);
        var result = await _sender.Send(query);
        return Ok(_mapper.Map<UserProblemsResponse>(result));
    }
}
