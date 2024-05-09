using Scheduler.Domain.GroupAggregate;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate.ValueObjects;
namespace Scheduler.Application.GroupInvites.Common;

public record GroupInviteResult(
    GroupId GroupId,
    GroupInviteId InviteId,
    UserId SenderId,
    UserGroupPermissions Permissions,
    string Message
);
