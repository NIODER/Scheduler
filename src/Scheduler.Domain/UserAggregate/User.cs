using Scheduler.Domain.Common;
using Scheduler.Domain.FinancialPlanAggregate.ValueObjects;
using Scheduler.Domain.FriendsInviteAggregate;
using Scheduler.Domain.FriendsInviteAggregate.ValueObjects;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.ProblemAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate.DomainEvents;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Domain.UserAggregate;

public class User : Aggregate<UserId>
{
    private readonly List<GroupId> _groupIds = [];
    private readonly List<FinancialPlanId> _financialPlanIds = [];
    private readonly List<FriendsInviteId> _receivedFriendsInviteIds = [];
    private readonly List<FriendsInviteId> _sendedFriendsInviteIds = [];
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
        List<FriendsInviteId> receivedFriendsInviteIds,
        List<FriendsInviteId> sendedFriendsInviteIds,
        List<ProblemId> problemIds) : base(userId)
    {
        Name = username;
        Email = email;
        Description = description;
        Settings = settings;
        PasswordHash = passwordHash;
        _groupIds = groupIds;
        _financialPlanIds = financialPlanIds;
        _receivedFriendsInviteIds = receivedFriendsInviteIds;
        _sendedFriendsInviteIds = sendedFriendsInviteIds;
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
    public IReadOnlyCollection<FriendsInviteId> FriendsInviteIds => _sendedFriendsInviteIds.Concat(_receivedFriendsInviteIds).ToList().AsReadOnly();
    public IReadOnlyCollection<FriendsInviteId> SendedFriendsInviteIds => _sendedFriendsInviteIds.AsReadOnly();
    public IReadOnlyCollection<FriendsInviteId> ReceivedFriendsInviteIds => _receivedFriendsInviteIds.AsReadOnly();
    public IReadOnlyCollection<ProblemId> ProblemIds => _problemIds.AsReadOnly();
    public IReadOnlyCollection<UserId> FriendsIds => _friendsIds.AsReadOnly();
    public IReadOnlyCollection<UserId> BlackListUserIds => _blackListUserIds.AsReadOnly();

    public void SetSettings(UserPrivateSettings settings)
    {
        Settings = settings;
    }

    public void AddReceivedFriendsInviteRequest(FriendsInviteId friendsInviteId)
    {
    }


    //public void AcceptFriendshipInvite(FriendsInviteId inviteId)
    //{
    //    if (!_receivedFriendsInviteIds.Any(fi => fi == inviteId))
    //    {
    //        throw new NullReferenceException($"No invite with id {inviteId.Value} found for user.");
    //    }
    //    _friendsIds.Add(invite.SenderId);
    //    _receivedFriendsInviteIds.Remove(invite.Id);
    //    AddDomainEvent(new UserAcceptedFriendInviteEvent(invite.SenderId, invite.AddressieId, invite.Id));
    //}

    //public void SendedFriendInviteAcceptedCallback(FriendsInvite invite)
    //{
    //    if (invite.AddressieId == Id)
    //    {
    //        throw new Exception($"Method {nameof(SendedFriendInviteAcceptedCallback)} only can be invoked for invite sender.");
    //    }
    //    if (invite.SenderId != Id)
    //    {
    //        throw new Exception($"Cannot execute {nameof(SendedFriendInviteAcceptedCallback)}, invalid sender id.");
    //    }
    //    _friendsIds.Add(invite.AddressieId);
    //    _receivedFriendsInviteIds.Remove(invite.Id);
    //}

    //public void AddFriendsInvite(FriendsInvite friendsInvite)
    //{
    //    _receivedFriendsInviteIds.Add(friendsInvite.Id);
    //}

    //public void DeleteFriendsInvite(FriendsInviteId friendsInviteId)
    //{
    //    _receivedFriendsInviteIds.Remove(friendsInviteId);
    //}
}