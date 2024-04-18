using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Domain.UserAggregate;
using Scheduler.Infrastructure.Persistance;
using Scheduler.Infrastructure.Persistance.Repositories;

namespace Scheduler.Infrastructure.Tests.RepositoriesTests;

public class UsersRepositoryTests
{
    private readonly IUsersRepository _usersRepository;

    public UsersRepositoryTests()
    {
        var context = new SchedulerDbContext(
                new DbContextOptionsBuilder<SchedulerDbContext>()
                    .UseNpgsql("Username=postgres;Host=localhost;Port=5432;Password=123123")
                    .Options
            );
        // context.Users.Add(User.Create(
        //     "username",
        //     "email",
        //     "descr",
        //     Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes("secretPassword123123")))
        // ));
        // context.SaveChanges();
        _usersRepository = new UsersRepository(context);
    }

    [Fact]
    public void AddTest()
    {
        User user = User.Create(
            "username1",
            "email1",
            "descr1",
            Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes("secretPassword123123")))
        );
        _usersRepository.Add(user);
        _usersRepository.SaveChanges();
    }

    [Fact]
    public void Update()
    {
        Assert.Equal("MTpY06kyJrm9it3CkAJp6BYpxtLhrnNU5510oyJYDe0=", Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes("secretPassword123123"))));
    }

    // User? GetUserByEmail(string email);
    // User? GetUserById(UserId userId);
    // Task<User?> GetUserByEmailAsync(string email);
    // Task<User?> GetUserByIdAsync(UserId userId);
    // List<User> GetUsersByGroupId(GroupId groupId);
    // Task<List<User>> GetUsersByGroupIdAsync(GroupId groupId);
    // void DeleteUserById(UserId userId);
    // int SaveChanges();
    // Task<int> SaveChangesAsync();
}