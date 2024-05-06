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
    private readonly List<FriendsInvite> _receivedFriendsInvites = [];
    private readonly List<FriendsInvite> _sendedFriendsInvites = [];
    private readonly List<ProblemId> _problemIds = [];
    private readonly List<UserId> _friendsIds = [];
    private readonly List<UserId> _blackListUserIds = [];

    public string Name { get; set; }
    public string Email { get; private set; }
    public string Description { get; set; } = string.Empty;
    public UserPrivateSettings Settings { get; set; }
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
        List<FriendsInvite> receivedFriendsInviteIds,
        List<FriendsInvite> sendedFriendsInviteIds,
        List<ProblemId> problemIds) : base(userId)
    {
        Name = username;
        Email = email;
        Description = description;
        Settings = settings;
        PasswordHash = passwordHash;
        _groupIds = groupIds;
        _financialPlanIds = financialPlanIds;
        _receivedFriendsInvites = receivedFriendsInviteIds;
        _sendedFriendsInvites = sendedFriendsInviteIds;
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
        receivedFriendsInviteIds: [],
        sendedFriendsInviteIds: [],
        problemIds: []
    );

    public IReadOnlyCollection<GroupId> GroupIds => _groupIds.AsReadOnly();
    public IReadOnlyCollection<FinancialPlanId> FinancialPlanIds => _financialPlanIds.AsReadOnly();
    public IReadOnlyCollection<FriendsInvite> FriendsInvites => _sendedFriendsInvites.Concat(_receivedFriendsInvites).ToList().AsReadOnly();
    public IReadOnlyCollection<FriendsInvite> SendedFriendsInvites => _sendedFriendsInvites.AsReadOnly();
    public IReadOnlyCollection<FriendsInvite> ReceivedFriendsInvites => _receivedFriendsInvites.AsReadOnly();
    public IReadOnlyCollection<ProblemId> ProblemIds => _problemIds.AsReadOnly();
    public IReadOnlyCollection<UserId> FriendsIds => _friendsIds.AsReadOnly();
    public IReadOnlyCollection<UserId> BlackListUserIds => _blackListUserIds.AsReadOnly();

    public void SetSettings(UserPrivateSettings settings)
    {
        Settings = settings;
    }

    public FriendsInvite SendFriendsInvite(User addressie, string message)
    {
        if (!addressie.Settings.HasFlag(UserPrivateSettings.OpenForFriendshipInvites))
        {
            throw new Exception("You can't send friendship invites to this user.");
        }
        var friendsInvite = FriendsInvite.Create(Id, addressie.Id, message);
        _sendedFriendsInvites.Add(friendsInvite);
        return friendsInvite;
    }

    public void AcceptFriendshipInvite(FriendsInviteId inviteId)
    {
        var invite = _receivedFriendsInvites.SingleOrDefault(fi => fi.Id == inviteId)
            ?? throw new NullReferenceException($"No invite with id {inviteId.Value} found for user.");
        _friendsIds.Add(invite.SenderId);
        _receivedFriendsInvites.Remove(invite);
        AddDomainEvent(new UserAcceptedFriendInviteEvent(SenderId: Id, AddressieId: invite.AddressieId));
    }

    public void AddFriend(UserId userId)
    {
        _friendsIds.Add(userId);
    }

    public FriendsInvite DeleteFriendsInvite(FriendsInviteId friendsInviteId)
    {
        if (!FriendsInvites.Any(fi => fi.Id == friendsInviteId))
        {
            throw new NullReferenceException($"No invite found with id {friendsInviteId.Value}.");
        }
        var invite = _receivedFriendsInvites.SingleOrDefault(fi => fi.Id == friendsInviteId);
        if (invite is not null)
        {
            _receivedFriendsInvites.Remove(invite);
            return invite;
        }
        invite = _sendedFriendsInvites.SingleOrDefault(fi => fi.Id == friendsInviteId);
        if (invite is not null)
        {
            _sendedFriendsInvites.Remove(invite);
            return invite;
        }
        throw new NullReferenceException($"No invite found with id {friendsInviteId.Value}.");
    }
}