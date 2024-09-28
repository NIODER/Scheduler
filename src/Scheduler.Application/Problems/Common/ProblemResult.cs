using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.ProblemAggregate;
using Scheduler.Domain.ProblemAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Application.Problems.Common;

public record ProblemResult(
    ProblemId TaskId,
    UserId CreatorId,
    UserId? UserId,
    GroupId? GroupId,
    string Title,
    string Description,
    ProblemStatus Status,
    DateTime Deadline
);
