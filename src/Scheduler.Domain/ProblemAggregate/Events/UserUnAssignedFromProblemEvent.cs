using Scheduler.Domain.Common.DomainDesign;
using Scheduler.Domain.ProblemAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Domain.ProblemAggregate.Events;

public record UserUnAssignedFromProblemEvent(
    ProblemId ProblemId,
    UserId UserId
) : IDomainEvent;
