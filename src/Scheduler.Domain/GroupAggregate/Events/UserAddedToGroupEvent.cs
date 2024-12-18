﻿using Scheduler.Domain.Common.DomainDesign;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Domain.GroupAggregate.Events;

public record UserAddedToGroupEvent(
    UserId UserId,
    GroupId GroupId
) : IDomainEvent;
