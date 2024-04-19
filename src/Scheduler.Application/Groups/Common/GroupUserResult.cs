using Scheduler.Domain.GroupAggregate;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Application.Groups.Common;

public record GroupUserResult(
    GroupId GroupId,
    UserId UserId,
    UserGroupPermissions Permissions
);