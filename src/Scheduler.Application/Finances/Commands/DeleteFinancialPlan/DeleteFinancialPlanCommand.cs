using MediatR;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Finances.Common;

namespace Scheduler.Application.Finances.Commands.DeleteFinancialPlan;

public record DeleteFinancialPlanCommand(Guid UserId, Guid FinancialPlanId) : IRequest<ICommandResult<FinancialPlanResult>>;
