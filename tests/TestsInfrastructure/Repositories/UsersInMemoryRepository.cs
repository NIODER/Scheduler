using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate;
using Scheduler.Domain.UserAggregate.Entities;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace TestsInfrastructure.Repositories;

internal class UsersInMemoryRepository : IUsersRepository
{
    public void Add(User user)
    {
        throw new NotImplementedException();
    }

    public void DeleteFriendsInvite(FriendsInvite friendsInvite)
    {
        throw new NotImplementedException();
    }

    public void DeleteUserById(UserId userId)
    {
        throw new NotImplementedException();
    }

    public User? GetUserByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetUserByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public User? GetUserById(UserId userId)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetUserByIdAsync(UserId userId)
    {
        throw new NotImplementedException();
    }

    public List<User> GetUsersByGroupId(GroupId groupId)
    {
        throw new NotImplementedException();
    }

    public Task<List<User>> GetUsersByGroupIdAsync(GroupId groupId)
    {
        throw new NotImplementedException();
    }

    public int SaveChanges()
    {
        throw new NotImplementedException();
    }

    public Task<int> SaveChangesAsync()
    {
        throw new NotImplementedException();
    }

    public void Update(User user)
    {
        throw new NotImplementedException();
    }
}
