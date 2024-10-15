using Scheduler.Domain.Common.DomainDesign;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate;
using Scheduler.Domain.UserAggregate.ValueObjects;
using System.Diagnostics;

namespace Scheduler.Domain.GroupAggregate.Entities;

public class GroupInvite : Entity<GroupInviteId>
{
    public UserId CreatorId { get; private set; }
    public GroupId GroupId { get; private set; }
    public UserGroupPermissions Permissions { get; private set; }
    public DateTime? Expire { get; private set; }
    public string Message { get; private set; }
    public uint? Usages { get; private set; }

    private GroupInvite()
    {
        CreatorId = default!;
        GroupId = default!;
        Message = null!;
    }

    private GroupInvite(
        GroupInviteId id,
        UserId creatorId,
        GroupId groupId,
        UserGroupPermissions permissions,
        DateTime? expire,
        string message,
        uint? usages
    ) : base(id)
    {
        CreatorId = creatorId;
        GroupId = groupId;
        Permissions = permissions;
        Expire = expire;
        Message = message;
        Usages = usages;
    }

    public static GroupInvite Create(
        UserId creatorId,
        GroupId groupId,
        UserGroupPermissions permissions,
        DateTime expire,
        string? message = null
    ) => new(
        new(Guid.NewGuid()),
        creatorId,
        groupId,
        permissions,
        expire,
        message ?? string.Empty,
        usages: null
    );

    public static GroupInvite Create(
        UserId creatorId,
        GroupId groupId,
        UserGroupPermissions permissions,
        uint usages,
        string? message = null
    ) => new(
        new(Guid.NewGuid()),
        creatorId,
        groupId,
        permissions,
        expire: null,
        message ?? string.Empty,
        usages
    );

    public static GroupInvite Create(
        UserId creatorId,
        GroupId groupId,
        UserGroupPermissions permissions,
        DateTime expire,
        uint usages,
        string? message = null
    ) => new(
        new(Guid.NewGuid()),
        creatorId,
        groupId,
        permissions,
        expire,
        message ?? string.Empty,
        usages
    );

    public bool IsActive(DateTime now)
    {
        if (Usages.HasValue)
        {
            return Usages != 0;
        }
        else if (Expire.HasValue)
        {
            return Expire.Value > now;
        }
        throw new UnreachableException("No Usages and Expire is setted.");
    }

    public bool UseAndIsActive(DateTime now)
    {
        if (!IsActive(now))
        {
            throw new Exception($"Invite {Id} is inactive.");
        }
        if (Usages.HasValue)
        {
            Usages--;
        }
        return IsActive(now);
    }
}