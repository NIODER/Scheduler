using MediatR;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Problems.Common;

namespace Scheduler.Application.Problems.Commands.DeleteProblem;

public record DeleteProblemCommand(
    Guid UserId,
    Guid ProblemId
) : IRequest<ICommandResult<ProblemResult>>;
