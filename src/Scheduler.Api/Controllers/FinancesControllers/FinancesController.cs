using Microsoft.AspNetCore.Mvc;
using Scheduler.Contracts.Finances;

namespace Scheduler.Api.Controllers.FinancesControllers
{
    [ApiController, Route("[controller]")]
    public class FinancesController : Controller
    {
        [HttpGet]
        public Task<IActionResult> GetUserFinancialPlansListAsync()
        {
            throw new NotImplementedException();
        }

        [HttpGet("group/{groupId}")]
        public Task<IActionResult> GetGroupFinancialPlansListAsync(Guid groupId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{financialPlanId}")]
        public Task<IActionResult> GetFinancialPlanGyIdAsync(Guid financialPlanId)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public Task<IActionResult> CreateFinancialPlanAsync([FromBody] FinancialPlanRequest request)
        {
            throw new NotImplementedException();
        }

        [HttpPost("{financialId}")]
        public Task<IActionResult> UpdateFinancialPlanAsync(Guid financialId, [FromQuery] string title)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{financialPlanId}")]
        public Task<IActionResult> DeleteFinancialPlanByIdAsync(Guid financialPlanId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{financialPlanId}/calculate/fill")]
        public Task<IActionResult> GetFillCalculatedFinancialPlanById(Guid financialPlanId, decimal budget, int priority)
        {
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
