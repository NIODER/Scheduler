using Microsoft.EntityFrameworkCore;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Domain.UserAggregate;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Infrastructure.Persistance.Repositories;

public sealed class UsersRepository(SchedulerDbContext context) : IUsersRepository
{
    private const string INVALID_USER_ID_EXCEPTION_MESSAGE = "No user found with given id";
    public void Add(User user)
    {
        context.Users.Add(user);
    }

    public void DeleteUserById(UserId userId)
    {
        var user = context.Users.SingleOrDefault(u => u.Id == userId) 
            ?? throw new NullReferenceException(INVALID_USER_ID_EXCEPTION_MESSAGE);
        context.Users.Remove(user);
    }

    public User GetUserByEmail(string email)
    {
        return context.Users.SingleOrDefault(u => u.Email == email)
            ?? throw new NullReferenceException(INVALID_USER_ID_EXCEPTION_MESSAGE);
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await context.Users.SingleOrDefaultAsync(u => u.Email == email)
            ?? throw new NullReferenceException(INVALID_USER_ID_EXCEPTION_MESSAGE);
    }

    public User GetUserById(UserId userId)
    {
        return context.Users.SingleOrDefault(u => u.Id == userId)
            ?? throw new NullReferenceException(INVALID_USER_ID_EXCEPTION_MESSAGE);
    }

    public async Task<User> GetUserByIdAsync(UserId userId)
    {
        return await context.Users.SingleOrDefaultAsync(u => u.Id == userId)
            ?? throw new NullReferenceException(INVALID_USER_ID_EXCEPTION_MESSAGE);
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
