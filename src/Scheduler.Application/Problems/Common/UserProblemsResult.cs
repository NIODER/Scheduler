using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Application.Problems.Common;

public record UserProblemsResult(
    UserId UserId,
    List<ProblemResult> Tasks
);
