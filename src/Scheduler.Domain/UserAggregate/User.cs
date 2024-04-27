using Scheduler.Domain.Common;
using Scheduler.Domain.FinancialPlanAggregate.ValueObjects;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.ProblemAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate.DomainEvents;
using Scheduler.Domain.UserAggregate.Entities;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Domain.UserAggregate;

public class User : Aggregate<UserId>
{
    private readonly List<GroupId> _groupIds = [];
    private readonly List<FinancialPlanId> _financialPlanIds = [];
    private readonly List<FriendsInvite> _friendsInvites = [];
    private readonly List<ProblemId> _problemIds = [];
    private readonly List<UserId> _friendsIds = [];
    private readonly List<UserId> _blackListUserIds = [];

    public string Name { get; set; }
    public string Email { get; private set; }
    public string Description { get; set; } = string.Empty;
    public UserPrivateSettings Settings { get; set; }
    // TODO: Add blacklist
    public string PasswordHash { get; private set; }

    private User(
        UserId userId,
        string username, 
        string email,
        string description,
        UserPrivateSettings settings,
        string passwordHash,
        List<GroupId> groupIds,
        List<FinancialPlanId> financialPlanIds,
        List<FriendsInvite> friendsInvites,
        List<ProblemId> problemIds) : base(userId)
    {
        Name = username;
        Email = email;
        Description = description;
        Settings = settings;
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
        string passwordHash,
        UserPrivateSettings? settings = null
    ) => new(
        new UserId(Guid.NewGuid()),
        username,
        email,
        description,
        settings ?? UserPrivateSettings.All,
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
    public IReadOnlyCollection<UserId> FriendsIds => _friendsIds.AsReadOnly();
    public IReadOnlyCollection<UserId> BlackListUserIds => _blackListUserIds.AsReadOnly();

    public void SetSettings(UserPrivateSettings settings)
    {
        Settings = settings;
    }

    public FriendsInvite AcceptFriendshipInvite(FriendsInviteId inviteId)
    {
        FriendsInvite invite = _friendsInvites.SingleOrDefault(i => i.Id == inviteId)
            ?? throw new NullReferenceException($"No invite with id {inviteId.Value} found for user.");
        if (invite.SenderId == Id)
        {
            throw new Exception("User can't accept friends invitation created by himself.");
        }
        _friendsIds.Add(invite.SenderId);
        _friendsInvites.Remove(invite);
        AddDomainEvent(new UserAcceptedFriendInviteEvent(invite.SenderId, invite.AddressieId, invite.Id));
        return invite;
    }

    public FriendsInvite SendedFriendInviteAccepted(FriendsInviteId inviteId)
    {
        FriendsInvite invite = _friendsInvites.SingleOrDefault(i => i.Id == inviteId)
            ?? throw new NullReferenceException($"No invite with id {inviteId.Value} found for user.");
        if (invite.AddressieId == Id)
        {
            throw new Exception($"Method {nameof(SendedFriendInviteAccepted)} only can be invoked for invite sender.");
        }
        _friendsIds.Add(invite.AddressieId);
        _friendsInvites.Remove(invite);
        return invite;
    }

    public void AddFriendsInvite(FriendsInvite friendsInvite)
    {
        _friendsInvites.Add(friendsInvite);
    }

    public FriendsInvite DeleteFriendsInvite(FriendsInviteId friendsInviteId)
    {
        var invite = _friendsInvites.SingleOrDefault(i => i.Id == friendsInviteId)
            ?? throw new NullReferenceException($"No invite with id {friendsInviteId.Value} found for user.");
        _friendsInvites.Remove(invite);
        return invite;
    }
}