using MediatR;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Problems.Common;

namespace Scheduler.Application.Problems.Commands.UpdateProblem;

public record UpdateProblemCommand(
    Guid ProblemId,
    Guid ExecutorId,
    Guid? UserId,
    string Title,
    string Description,
    int? Status,
    DateTime Deadline
) : IRequest<ICommandResult<ProblemResult>>;
