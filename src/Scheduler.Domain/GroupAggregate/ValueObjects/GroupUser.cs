using Scheduler.Domain.UserAggregate;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Domain.GroupAggregate.ValueObjects;

public record GroupUser(UserId UserId, GroupId GroupId, UserGroupPermissions Permissions);