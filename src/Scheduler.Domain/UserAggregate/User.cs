using Scheduler.Domain.Common;
using Scheduler.Domain.FinancialPlanAggregate.ValueObjects;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.TaskAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate.Entities;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Domain.UserAggregate;

public class User : Aggregate<UserId>
{
    private readonly List<GroupId> _groupIds = [];
    private readonly List<FinancialPlanId> _financialPlanIds = [];
    private readonly List<FriendsInvite> _friendsInvites = [];
    private readonly List<TaskId> _taskIds = [];

    public string Username { get; private set; }
    public string Email { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; }

    private User(
        UserId userId,
        string username, 
        string email,
        string description,
        string passwordHash,
        List<GroupId> groupIds,
        List<FinancialPlanId> financialPlanIds,
        List<FriendsInvite> friendsInvites,
        List<TaskId> taskIds) : base(userId)
    {
        Username = username;
        Email = email;
        Description = description;
        PasswordHash = passwordHash;
        _groupIds = groupIds;
        _financialPlanIds = financialPlanIds;
        _friendsInvites = friendsInvites;
        _taskIds = taskIds;
    }
        
    private User()
    {
        Username = null!;
        Email = null!;
        Description = null!;
        PasswordHash = null!;
    }

    public static User Create(
        string username,
        string email,
        string description,
        string passwordHash
    ) => new(
        new UserId(Guid.NewGuid()),
        username,
        email,
        description,
        passwordHash,
        groupIds: [],
        financialPlanIds: [],
        friendsInvites: [],
        taskIds: []
    );

    public IReadOnlyCollection<GroupId> GroupIds => _groupIds.AsReadOnly();
    public IReadOnlyCollection<FinancialPlanId> FinancialPlanIds => _financialPlanIds.AsReadOnly();
    public IReadOnlyCollection<FriendsInvite> FriendsInvites => _friendsInvites.AsReadOnly();
    public IReadOnlyCollection<TaskId> TaskIds => _taskIds.AsReadOnly();

    // TODO Write behavior
}