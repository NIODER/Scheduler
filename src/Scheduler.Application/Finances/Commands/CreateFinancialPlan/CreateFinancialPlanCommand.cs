using MediatR;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Finances.Common;

namespace Scheduler.Application.Finances.Commands.CreateFinancialPlan;

public record CreateFinancialPlanCommand(
    Guid UserId,
    Guid? GroupId,
    string Title
) : IRequest<ICommandResult<FinancialPlanResult>>;
