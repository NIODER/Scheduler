using MediatR;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Problems.Common;

namespace Scheduler.Application.Problems.Commands.CreateProblem;

public record CreateProblemCommand(
    Guid CreatorId,
    Guid? UserId,
    Guid? GroupId,
    string Title,
    string Description,
    int? Status,
    DateTime Deadline
) : IRequest<ICommandResult<ProblemResult>>;
