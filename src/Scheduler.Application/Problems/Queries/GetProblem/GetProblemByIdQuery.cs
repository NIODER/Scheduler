using MediatR;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Problems.Common;

namespace Scheduler.Application.Problems.Queries.GetProblem;

public record GetProblemByIdQuery(
    Guid ExecutorId,
    Guid TaskId
) : IRequest<ICommandResult<ProblemResult>>;
