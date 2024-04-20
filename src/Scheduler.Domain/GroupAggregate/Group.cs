using System.Diagnostics;
using Scheduler.Domain.Common;
using Scheduler.Domain.FinancialPlanAggregate.ValueObjects;
using Scheduler.Domain.GroupAggregate.Entities;
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
    public string GroupName { get; set; }

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
        var user = _users.SingleOrDefault(u => u.UserId == userId)
            ?? throw new NullReferenceException($"No user with id {userId.Value} found in group.");
        if (UserHasPermissions(userId, UserGroupPermissions.IsGroupOwner))
        {
            throw new Exception("Cannot delete group owner from group.");
        }
        _users.Remove(user);
    }
}
