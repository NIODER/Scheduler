using Scheduler.Domain.Common;
using Scheduler.Domain.FinancialPlanAggregate.ValueObjects;
using Scheduler.Domain.GroupAggregate.Entities;
using Scheduler.Domain.GroupAggregate.Events;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.ProblemAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Domain.GroupAggregate;

public class Group : Aggregate<GroupId>
{
    private readonly List<GroupUser> _users = [];
    private readonly List<GroupInvite> _invites = [];
    private readonly List<ProblemId> _problemIds = [];
    private readonly List<FinancialPlanId> _financialPlanIds = [];

    private Group()
    {
        GroupName = default!;
    }

    private Group(
        GroupId groupId,
        string groupName,
        List<GroupUser> users,
        List<GroupInvite> invites,
        List<ProblemId> problemIds,
        List<FinancialPlanId> financialPlanIds
    ) : base(groupId)
    {
        GroupName = groupName;
        _users = users;
        _invites = invites;
        _problemIds = problemIds;
        _financialPlanIds = financialPlanIds;
    }

    public static Group Create(string groupName) => new(
        new GroupId(Guid.NewGuid()),
        groupName, 
        users: [],
        invites: [],
        problemIds: [],
        financialPlanIds: []);

    public string GroupName { get; set; }
    public IReadOnlyCollection<GroupUser> Users => _users.AsReadOnly();
    public IReadOnlyCollection<GroupInvite> Invites => _invites.AsReadOnly();
    public IReadOnlyCollection<ProblemId> ProblemIds => _problemIds.AsReadOnly();
    public IReadOnlyCollection<FinancialPlanId> FinancialPlanIds => _financialPlanIds.AsReadOnly();

    public bool UserHasPermissions(UserId userId, UserGroupPermissions permissions)
    {
        var user = _users.FirstOrDefault(x => x.UserId == userId);
        if (user is not null && user.Permissions.HasFlag(permissions))
        {
            return true;
        }
        return false;
    }

    public void AddUser(UserId userId, UserGroupPermissions permissions)
    {
        var groupUser = new GroupUser(userId, Id, permissions);
        _users.Add(groupUser);
        AddDomainEvent(new UserAddedToGroupEvent(userId, Id));
    }

    public void UpdateUser(GroupUser groupUser)
    {
        if (groupUser.GroupId != Id)
        {
            throw new ArgumentException("Group id must be equal to groupUser.GroupId", nameof(groupUser));
        }
        var user = _users.SingleOrDefault(x => x.UserId.Value == groupUser.UserId.Value)
            ?? throw new NullReferenceException($"No user with id {groupUser.UserId.Value}");
        _users.Remove(user);
        _users.Add(groupUser);
    }

    public void DeleteUser(UserId userId)
    {
        GroupUser? user = _users.SingleOrDefault(u => u.UserId == userId);
        if (user is null)
        {
            return;
        }
        if (UserHasPermissions(userId, UserGroupPermissions.IsGroupOwner))
        {
            throw new InvalidOperationException("Cannot delete group owner from group.");
        }
        _users.Remove(user);
        AddDomainEvent(new UserDeletedFromGroupEvent(Id, user.UserId));
    }

    public void AcceptUserInGroup(UserId userId, GroupInviteId inviteId, DateTime now)
    {
        GroupInvite invite = _invites.SingleOrDefault(i => i.Id == inviteId)
            ?? throw new NullReferenceException($"No invite with id {inviteId.Value} found");
        if (!invite.IsActive(now))
        {
            _invites.Remove(invite);
            throw new Exception($"Group invite with id {inviteId.Value} is inactive and deleted.");
        }
        if (!invite.UseAndIsActive(now))
        {
            _invites.Remove(invite);
        }
        AddUser(userId, invite.Permissions);
    }

    public GroupInvite CreateInvite(UserId creatorId, UserGroupPermissions userGroupPermissions, string? message, DateTime? now = null, uint? usages = null)
    {
        GroupInvite groupInvite = (now is not null, usages is not null) switch
        {
            (true, false) => GroupInvite.Create(creatorId, Id, userGroupPermissions, now!.Value, message),
            (false, true) => GroupInvite.Create(creatorId, Id, userGroupPermissions, usages!.Value, message),
            (true, true) => GroupInvite.Create(creatorId, Id, userGroupPermissions, now!.Value, usages!.Value, message),
            (false, false) => throw new NullReferenceException($"Nor of creation time {nameof(now)} and {nameof(usages)} are setted.")
        };
        _invites.Add(groupInvite);
        return groupInvite;
    }

    public GroupInvite DeleteInvite(GroupInviteId inviteId)
    {
        GroupInvite invite = _invites.SingleOrDefault(i => i.Id == inviteId)
            ?? throw new NullReferenceException($"No invite with id {inviteId.Value} found.");
        _invites.Remove(invite);
        return invite;
    }

    public void RemoveProblem(ProblemId problemId)
    {
        _problemIds.Remove(problemId);
    }
}
