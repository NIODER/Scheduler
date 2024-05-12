using MediatR;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Problems.Common;

namespace Scheduler.Application.Problems.Queries.GetGroupProblems;

public record GetGroupProblemsCommand(
    Guid GroupId,
    Guid UserId
) : IRequest<ICommandResult<GroupProblemsResult>>;
