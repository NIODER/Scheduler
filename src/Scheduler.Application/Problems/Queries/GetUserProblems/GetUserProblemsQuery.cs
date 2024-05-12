using MediatR;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Problems.Common;

namespace Scheduler.Application.Problems.Queries.GetUserProblems;

public record GetUserProblemsQuery(
    Guid UserId
) : IRequest<ICommandResult<UserProblemsResult>>;
