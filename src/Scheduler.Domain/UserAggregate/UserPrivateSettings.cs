using System.ComponentModel;

namespace Scheduler.Domain.UserAggregate;

[Flags, DefaultValue(All)]
public enum UserPrivateSettings
{
    OpenForFriendsGroupInvites = 1 << 0,
    OpenForFriendshipInvites = 1 << 1,
    All = ~0
}