using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Api.Common.Utils;
using Scheduler.Application.Finances.Commands.CreateFinancialPlan;
using Scheduler.Application.Finances.Commands.DeleteFinancialPlan;
using Scheduler.Application.Finances.Commands.UpdateFinancialPlan;
using Scheduler.Application.Finances.Common;
using Scheduler.Application.Finances.Queries.GetFillCalculatedFinancialPlan;
using Scheduler.Application.Finances.Queries.GetFinancialPlanById;
using Scheduler.Application.Finances.Queries.GetGroupFinancialPlans;
using Scheduler.Application.Finances.Queries.GetUserFinancialPlans;
using Scheduler.Contracts.Finances;

namespace Scheduler.Api.Controllers.FinancesControllers;

[ApiController, Route("[controller]"), Authorize]
public class FinancesController(ISender sender, IMapper mapper, ILogger<FinancesController> logger) : Controller
{
    [ApiController, Route("[controller]")]
    public class FinancesController : Controller
    {
    [HttpGet]
    public async Task<IActionResult> GetUserFinancialPlansListAsync()
    {
        var userId = HttpContext.GetExecutorUserId();
        if (userId is null)
        {
            return Forbid();
        }
        var query = new GetUserFinancialPlansQuery(userId.Value);
        var result = await sender.Send(query);
        return result.ActionResult<UserFinancialPlansListResult, FinancialPlansListResponse>(mapper, logger);
    }

    [HttpGet("group/{groupId}")]
    public async Task<IActionResult> GetGroupFinancialPlansListAsync(Guid groupId)
    {
        var userId = HttpContext.GetExecutorUserId();
        if (userId is null)
        {
            return Forbid();
        }
        var query = new GetGroupFinancialPlansListQuery(userId.Value, groupId);
        var result = await sender.Send(query);
        return result.ActionResult<GroupFinancialPlansListResult, FinancialPlansListResponse>(mapper, logger);
    }

    [HttpGet("{financialPlanId}")]
    public async Task<IActionResult> GetFinancialPlanGyIdAsync(Guid financialPlanId)
    {
        var userId = HttpContext.GetExecutorUserId();
        if (userId is null)
        {
            return Forbid();
        }
        var query = new GetFinancialPlanByIdQuery(userId.Value, financialPlanId);
        var result = await sender.Send(query);
        return result.ActionResult<FinancialPlanResult, FinancialPlanResponse>(mapper, logger);
    }

    [HttpPut]
    public async Task<IActionResult> CreateFinancialPlanAsync([FromBody] FinancialPlanRequest request)
    {
        var userId = HttpContext.GetExecutorUserId();
        if (userId is null)
        {
            return Forbid();
        }
        var command = new CreateFinancialPlanCommand(userId.Value, request.GroupId, request.Title);
        var result = await sender.Send(command);
        return result.ActionResult<FinancialPlanResult, FinancialPlanResponse>(mapper, logger);
    }

    [HttpPost("{financialId}")]
    public async Task<IActionResult> UpdateFinancialPlanAsync(Guid financialId, [FromBody] UpdateFinancialPlanRequest request)
    {
        var userId = HttpContext.GetExecutorUserId();
        if (userId is null)
        {
            return Forbid();
        }
        var command = new UpdateFinancialPlanCommand(userId.Value, financialId, request.Title);
        var result = await sender.Send(command);
        return result.ActionResult<FinancialPlanResult, FinancialPlanResponse>(mapper, logger);
    }

    [HttpDelete("{financialPlanId}")]
    public async Task<IActionResult> DeleteFinancialPlanByIdAsync(Guid financialPlanId)
    {
        var userId = HttpContext.GetExecutorUserId();
        if (userId is null)
        {
            return Forbid();
        }
        var command = new DeleteFinancialPlanCommand(userId.Value, financialPlanId);
        var result = await sender.Send(command);
        return result.ActionResult<FinancialPlanResult, FinancialPlanResponse>(mapper, logger);
    }

    [HttpGet("{financialPlanId}/calculate/fill")]
    public async Task<IActionResult> GetFillCalculatedFinancialPlanById(Guid financialPlanId, decimal budget, int priority, DateTime? origin)
    {
        var userId = HttpContext.GetExecutorUserId();
        if (userId is null)
        {
            return Forbid();
        }
        var command = new GetFillCalculatedFinancialPlanQuery(userId.Value, financialPlanId, budget, priority, origin);
        var result = await sender.Send(command);
        throw new NotImplementedException();
    }

    [HttpGet("{financialPlanId}/calculate/distribute")]
    public Task<IActionResult> GetDistributeFinancialPlanById(Guid financialPlanId, decimal budget, DateTime date)
    {
        throw new NotImplementedException();
    }

    [HttpGet("charges/{chargeId}")]
    public Task<IActionResult> GetChargeByIdAsync(Guid chargeId)
    {
        throw new NotImplementedException();
    }

    [HttpPut("{financialId}/charges")]
    public Task<IActionResult> CreateChargeAsync(Guid financialId, [FromBody] ChargeRequest request)
    {
        throw new NotImplementedException();
    }

    [HttpPost("{financialId}/charges/{chargeId}")]
    public Task<IActionResult> UpdateActionResultAsync(Guid financialId, Guid chargeId, [FromBody] ChargeRequest request)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("charges/{chargeId}")]
    public Task<IActionResult> DeleteChargeAsync(Guid chargeId)
    {
        throw new NotImplementedException();
    }
}
}
