using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scheduler.Api;
using Scheduler.Application;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Domain.UserAggregate;
using Scheduler.Domain.UserAggregate.Entities;
using Scheduler.Infrastructure.Persistance;
using Scheduler.Infrastructure.Persistance.Interceptors;
using Scheduler.Infrastructure.Persistance.Repositories;

namespace Scheduler.Infrastructure.Tests.RepositoriesTests;

public class UsersRepositoryTests
{
    private readonly IUsersRepository _usersRepository;

    public UsersRepositoryTests()
    {
        var services = new ServiceCollection();
        services.AddPresentation();
        services.AddApplication();
        services.AddInfrastructure(new ConfigurationBuilder()
            .AddJsonFile(@"D:\MyProgs\Scheduler\src\Scheduler.Api\appsettings.Development.json", optional: false)
            .Build());
        var context = new SchedulerDbContext(
                new DbContextOptionsBuilder<SchedulerDbContext>()
                    .UseNpgsql("Username=postgres;Host=localhost;Port=5432;Password=123123")
                    .Options,
                services.BuildServiceProvider().GetRequiredService<PublishDomainEventsInterceptor>()
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

    [Fact]
    public void UserHasSendedAndReceivedFriendInvites()
    {
        User sender = User.Create(
            "Username1",
            "alex@gmail.com",
            "sadf",
            Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes("secretPassword123123"))));
        User addressie = User.Create(
            "Username11",
            "a1lex@gmail.com",
            "sadf",
            Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes("secretPassword123123"))));
        _usersRepository.Add(sender);
        _usersRepository.Add(addressie);
        FriendsInvite fi = sender.SendFriendsInvite(addressie, "message");
        _usersRepository.SaveChanges();
        sender = _usersRepository.GetUserById(sender.Id) ?? throw new NullReferenceException();
        addressie = _usersRepository.GetUserById(addressie.Id) ?? throw new NullReferenceException();
        Assert.Single(sender.SendedFriendsInvites, fi);
        Assert.Single(addressie.ReceivedFriendsInvites, fi);
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