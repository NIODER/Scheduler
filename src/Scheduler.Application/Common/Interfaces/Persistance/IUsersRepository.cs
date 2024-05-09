using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate;
using Scheduler.Domain.UserAggregate.Entities;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Application.Common.Interfaces.Persistance;

public interface IUsersRepository
{
    void Add(User user);
    void Update(User user);
    User? GetUserByEmail(string email);
    User? GetUserById(UserId userId);
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByIdAsync(UserId userId);
    List<User> GetUsersByGroupId(GroupId groupId);
    Task<List<User>> GetUsersByGroupIdAsync(GroupId groupId);
    void DeleteUserById(UserId userId);
    void DeleteFriendsInvite(FriendsInvite friendsInvite);
    int SaveChanges();
    Task<int> SaveChangesAsync();
}