using Microsoft.EntityFrameworkCore;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Domain.GroupAggregate;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Infrastructure.Persistance.Repositories;

public sealed class GroupsRepository(SchedulerDbContext context) : IGroupsRepository
{
    public void Add(Group group)
    {
        context.Groups.Add(group);
    }

    public void DeleteGroupById(GroupId groupId)
    {
        var group = context.Groups.SingleOrDefault(g => g.Id == groupId);
        if (group is not null)
        {
            context.Groups.Remove(group);
        }
    }

    public Group? GetGroupById(GroupId groupId)
    {
        return context.Groups.SingleOrDefault(g => g.Id == groupId);
    }

    public Task<Group?> GetGroupByIdAsync(GroupId groupId)
    {
        return context.Groups.SingleOrDefaultAsync(g => g.Id == groupId);
    }

    public List<Group> GetGroupsByUserId(UserId userId)
    {
        return context.Groups
            .Where(g => g.Users.Any(u => u.UserId == userId))
            .ToList();
    }

    public Task<List<Group>> GetGroupsByUserIdAsync(UserId userId)
    {
        return context.Groups.Where(g => g.Users.Any(u => u.UserId == userId))
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

    public void Update(Group group)
    {
        context.Update(group);
    }
}
