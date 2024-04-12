using Scheduler.Domain.Common;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.GroupInviteAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Domain.GroupInviteAggregate;

public class GroupInvite : Aggregate<GroupInviteId>
{
    public UserId SenderId { get; private set; }
    public GroupId GroupId { get; private set; }
    public UserId? AddressieId { get; private set; }
    public UserGroupPermissions Permissions { get; private set; }
    public string Message { get; private set; }

    private GroupInvite()
    {
        SenderId = default!;
        GroupId = default!;
        Message = null!;
    }

    private GroupInvite(
        GroupInviteId groupInviteId,
        UserId senderId,
        GroupId groupId,
        UserId? addressieId,
        UserGroupPermissions permissions,
        string message
    ) : base(groupInviteId)
    {
        SenderId = senderId;
        GroupId = groupId;
        AddressieId = addressieId;
        Permissions = permissions;
        Message = message;
    }

    public static GroupInvite Create(
        UserId senderId,
        GroupId groupId,
        UserGroupPermissions permissions,
        string message,
        UserId? addressieId = null
    ) => new(GroupInviteId.CreateUnique(), senderId, groupId, addressieId, permissions, message);
}