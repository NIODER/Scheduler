using Scheduler.Domain.Common;
using Scheduler.Domain.UserAggregate;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Domain.GroupAggregate.ValueObjects;

public class GroupUser : ValueObject
{
    public UserId UserId { get; private set; }
    public UserGroupPermissions Permissions { get; private set; }

    private GroupUser(
        UserId userId,
        UserGroupPermissions permissions)
    {
        UserId = userId;
        Permissions = permissions;
    }

    private GroupUser()
    {
        UserId = default!;
    }

    public static GroupUser Create(
        UserId userId,
        UserGroupPermissions permissions
    ) => new(userId, permissions);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return UserId;
        yield return Permissions;
    }
}