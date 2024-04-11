using Scheduler.Domain.Common;
using Scheduler.Domain.GroupAggregate.ValueObjects;

namespace Scheduler.Domain.UserAggregate.ValueObjects;

public sealed class UserGroup : ValueObject
{
    public GroupId GroupId { get; private set; }
    public UserGroupPermissions Permissions { get; private set; }

    private UserGroup()
    {
        GroupId = default!;
    }

    private UserGroup(GroupId groupId, UserGroupPermissions permissions)
    {
        GroupId = groupId;
        Permissions = permissions;
    }

    public static UserGroup Create(GroupId groupId, UserGroupPermissions userGroupPermissions)
    {
        return new(groupId, userGroupPermissions);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return GroupId;
        yield return Permissions;
    }
}
