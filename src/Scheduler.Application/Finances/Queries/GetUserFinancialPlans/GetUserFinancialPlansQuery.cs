using MediatR;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Finances.Common;

namespace Scheduler.Application.Finances.Queries.GetUserFinancialPlans;

public record GetUserFinancialPlansQuery(Guid UserId) : IRequest<ICommandResult<UserFinancialPlansListResult>>;
