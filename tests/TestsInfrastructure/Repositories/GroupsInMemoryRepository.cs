using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Domain.GroupAggregate;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace TestsInfrastructure.Repositories;

internal class GroupsInMemoryRepository : IGroupsRepository
{
    public void Add(Group group)
    {
        throw new NotImplementedException();
    }

    public void DeleteGroupById(GroupId groupId)
    {
        throw new NotImplementedException();
    }

    public Group? GetGroupById(GroupId groupId)
    {
        throw new NotImplementedException();
    }

    public Task<Group?> GetGroupByIdAsync(GroupId groupId)
    {
        throw new NotImplementedException();
    }

    public List<Group> GetGroupsByUserId(UserId userId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Group>> GetGroupsByUserIdAsync(UserId userId)
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

    public void Update(Group group)
    {
        throw new NotImplementedException();
    }
}
