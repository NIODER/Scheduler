using Scheduler.Domain.Common;
using Scheduler.Domain.ProblemAggregate.ValueObjects;

namespace Scheduler.Domain.ProblemAggregate.Events;

public record ProblemDeletedEvent(ProblemId ProblemId) : IDomainEvent;
