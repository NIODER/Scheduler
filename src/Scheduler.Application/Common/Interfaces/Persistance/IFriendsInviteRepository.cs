using Scheduler.Domain.FriendsInviteAggregate;
using Scheduler.Domain.FriendsInviteAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Application.Common.Interfaces.Persistance;

public interface IFriendsInviteRepository
{
    FriendsInvite? GetFriendsInviteById(FriendsInviteId friendsInviteId);
    Task<FriendsInvite?> GetFriendsInviteByIdAsync(FriendsInviteId friendsInviteId);
    List<FriendsInvite> GetSendedUserInvitesByUserId(UserId userId);
    Task<List<FriendsInvite>> GetSendedUserInvitesByUserIdAsync(UserId userId);
    List<FriendsInvite> GetReceivedUserInvitesByUserId(UserId userId);
    Task<List<FriendsInvite>> GetReceivedUserInvitesByUserIdAsync(UserId userId);
    List<FriendsInvite> GetRelatedUserInvitesByUserId(UserId userId);
    Task<List<FriendsInvite>> GetRelatedUserInvitesByUserIdAsync(UserId userId);
    void DeleteFriendsInviteById(FriendsInviteId friendsInviteId);
    void CreateFriendsInvite(FriendsInvite friendsInvite);
    int SaveChanges();
    Task<int> SaveChangesAsync();
}