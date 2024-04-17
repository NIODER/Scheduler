using Microsoft.EntityFrameworkCore;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Domain.GroupAggregate;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Infrastructure.Persistance.Repositories;

public sealed class GroupsRepository(SchedulerDbContext context) : IGroupsRepository
{
    private const string INVALID_GROUP_ID_EXCEPTION_MESSAGE = "No group with given id found.";

    public void Add(Group group)
    {
        context.Groups.Add(group);
    }

    public void DeleteGroupById(GroupId groupId)
    {
        var group = context.Groups.SingleOrDefault(g => g.Id == groupId)
            ?? throw new NullReferenceException(INVALID_GROUP_ID_EXCEPTION_MESSAGE);
        context.Groups.Remove(group);
    }

    public Group GetGroupById(GroupId groupId)
    {
        return context.Groups.SingleOrDefault(g => g.Id == groupId)
            ?? throw new NullReferenceException(INVALID_GROUP_ID_EXCEPTION_MESSAGE);
    }

    public async Task<Group> GetGroupByIdAsync(GroupId groupId)
    {
        return await context.Groups.SingleOrDefaultAsync(g => g.Id == groupId)
            ?? throw new NullReferenceException(INVALID_GROUP_ID_EXCEPTION_MESSAGE);
    }

    public IEnumerable<Group> GetGroupsByUserId(UserId userId)
    {
        return context.Groups.Where(g => g.Users.Any(u => u.UserId == userId))
            .AsEnumerable();
    }

    public IAsyncEnumerable<Group> GetGroupsByUserIdAsync(UserId userId)
    {
        return context.Groups.Where(g => g.Users.Any(u => u.UserId == userId))
            .AsAsyncEnumerable();
    }

    public int SaveChanges()
    {
        return context.SaveChanges();
    }

    public Task<int> SaveChangesAsync()
    {
        return context.SaveChangesAsync();
    }

    public void Update(Group group)
    {
        context.Update(group);
    }
}
