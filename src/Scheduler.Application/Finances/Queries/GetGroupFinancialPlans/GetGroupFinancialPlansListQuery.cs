using MediatR;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Finances.Common;

namespace Scheduler.Application.Finances.Queries.GetGroupFinancialPlans;

public record GetGroupFinancialPlansListQuery(Guid UserId, Guid GroupId) : IRequest<ICommandResult<GroupFinancialPlansListResult>>;
