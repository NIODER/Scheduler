using System.Runtime.InteropServices;
using Scheduler.Domain.Common;
using Scheduler.Domain.FinancialPlanAggregate.ValueObjects;
using Scheduler.Domain.FriendsInviteAggregate.ValueObjects;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.GroupInviteAggregate.ValueObjects;
using Scheduler.Domain.TaskAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Domain.UserAggregate;

public class User : Aggregate<UserId>
{
    private readonly List<UserGroup> _groups = [];
    private readonly List<FinancialPlanId> _financialPlanIds = [];
    private readonly List<FriendsInviteId> _friendsInviteIds = [];
    private readonly List<GroupInviteId>  _groupInviteIds = [];
    private readonly List<TaskId> _taskIds = [];

    public string Username { get; private set; }
    public string Email { get; private set; }
    public string Description { get; private set; }
    public string PasswordHash { get; private set; }

    private User(
        string username, 
        string email,
        string description,
        string passwordHash,
        List<UserGroup> groups,
        List<FinancialPlanId> financialPlanIds,
        List<FriendsInviteId> friendsInviteIds,
        List<GroupInviteId> groupInviteIds,
        List<TaskId> taskIds)
    {
        Username = username;
        Email = email;
        Description = description;
        PasswordHash = passwordHash;
        _groups = groups;
        _financialPlanIds = financialPlanIds;
        _friendsInviteIds = friendsInviteIds;
        _groupInviteIds = groupInviteIds;
        _taskIds = taskIds;
    }
        
    private User()
    {
        Username = null!;
        Email = null!;
        Description = null!;
        PasswordHash = null!;
    }

    // TODO write methods for creation

    public IReadOnlyCollection<GroupId> GroupIds => _groups
        .Select(g => g.GroupId)
        .ToList()
        .AsReadOnly();

    public IReadOnlyCollection<UserGroup> UserGroups => _groups.AsReadOnly();
    public IReadOnlyCollection<FinancialPlanId> FinancialPlanIds => _financialPlanIds.AsReadOnly();
    public IReadOnlyCollection<FriendsInviteId> FriendsInviteIds => _friendsInviteIds.AsReadOnly();
    public IReadOnlyCollection<GroupInviteId> GroupInviteIds => _groupInviteIds.AsReadOnly();
    public IReadOnlyCollection<TaskId> TaskIds => _taskIds.AsReadOnly();

    // TODO write methods for behavior
}