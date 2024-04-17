using Microsoft.EntityFrameworkCore;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Infrastructure.Persistance.Repositories;

public sealed class UsersRepository(SchedulerDbContext context) : IUsersRepository
{
    public void Add(User user)
    {
        context.Users.Add(user);
    }

    public void DeleteUserById(UserId userId)
    {
        var user = context.Users.SingleOrDefault(u => u.Id == userId);
        if (user is not null)
        {
            context.Users.Remove(user);
        }
    }

    public User? GetUserByEmail(string email)
    {
        return context.Users.SingleOrDefault(u => u.Email == email);
    }

    public Task<User?> GetUserByEmailAsync(string email)
    {
        return context.Users.SingleOrDefaultAsync(u => u.Email == email);
    }

    public User? GetUserById(UserId userId)
    {
        return context.Users.SingleOrDefault(u => u.Id == userId);
    }

    public Task<User?> GetUserByIdAsync(UserId userId)
    {
        return context.Users.SingleOrDefaultAsync(u => u.Id == userId);
    }

    public List<User> GetUsersByGroupId(GroupId groupId)
    {
        return context.Users
            .Include(u => u.GroupIds)
            .Where(u => u.GroupIds.Contains(groupId))
            .ToList();
    }

    public Task<List<User>> GetUsersByGroupIdAsync(GroupId groupId)
    {
        return context.Users
            .Include(u => u.GroupIds)
            .Where(u => u.GroupIds.Contains(groupId))
            .ToListAsync();
    }

    public int SaveChanges()
    {
        return context.SaveChanges();
    }

    public Task<int> SaveChangesAsync()
    {
        return context.SaveChangesAsync();
    }

    public void Update(User user)
    {
        context.Users.Update(user);
    }
}
