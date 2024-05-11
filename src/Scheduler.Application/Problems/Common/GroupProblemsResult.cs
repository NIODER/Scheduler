using Scheduler.Domain.GroupAggregate.ValueObjects;

namespace Scheduler.Application.Problems.Common;

public record GroupProblemsResult(
    GroupId GroupId,
    List<ProblemResult> Tasks
);
