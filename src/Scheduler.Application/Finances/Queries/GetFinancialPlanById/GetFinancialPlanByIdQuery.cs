using MediatR;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Finances.Common;

namespace Scheduler.Application.Finances.Queries.GetFinancialPlanById;

public record GetFinancialPlanByIdQuery(Guid UserId, Guid FinancialId) : IRequest<ICommandResult<FinancialPlanResult>>;
