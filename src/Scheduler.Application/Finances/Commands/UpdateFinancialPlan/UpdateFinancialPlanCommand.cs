using MediatR;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Finances.Common;

namespace Scheduler.Application.Finances.Commands.UpdateFinancialPlan;

public record UpdateFinancialPlanCommand(Guid UserId, Guid FinancialId, string Title) : IRequest<ICommandResult<FinancialPlanResult>>;
