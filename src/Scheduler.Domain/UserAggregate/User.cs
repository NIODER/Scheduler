using Scheduler.Domain.Common;
using Scheduler.Domain.FinancialPlanAggregate.ValueObjects;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.ProblemAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate.Entities;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Domain.UserAggregate;

public class User : Aggregate<UserId>
{
    private readonly List<GroupId> _groupIds = [];
    private readonly List<FinancialPlanId> _financialPlanIds = [];
    private readonly List<FriendsInvite> _friendsInvites = [];
    private readonly List<ProblemId> _problemIds = [];

    public string Name { get; set; }
    public string Email { get; private set; }
    public string Description { get; set; } = string.Empty;
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
        List<ProblemId> problemIds) : base(userId)
    {
        Name = username;
        Email = email;
        Description = description;
        PasswordHash = passwordHash;
        _groupIds = groupIds;
        _financialPlanIds = financialPlanIds;
        _friendsInvites = friendsInvites;
        _problemIds = problemIds;
    }
        
    private User()
    {
        Name = null!;
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
        problemIds: []
    );

    public IReadOnlyCollection<GroupId> GroupIds => _groupIds.AsReadOnly();
    public IReadOnlyCollection<FinancialPlanId> FinancialPlanIds => _financialPlanIds.AsReadOnly();
    public IReadOnlyCollection<FriendsInvite> FriendsInvites => _friendsInvites.AsReadOnly();
    public IReadOnlyCollection<ProblemId> ProblemIds => _problemIds.AsReadOnly();
}