using Scheduler.Domain.GroupAggregate;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Application.Common.Interfaces.Persistance;

public interface IGroupsRepository
{
    void Add(Group group);
    void Update(Group group);
    Group GetGroupById(GroupId groupId);
    Task<Group> GetGroupByIdAsync(GroupId groupId);
    IEnumerable<Group> GetGroupsByUserId(UserId userId);
    Task<IEnumerable<Group>> GetGroupsByUserIdAsync(UserId userId);
    void DeleteGroupById(GroupId groupId);
    int SaveChanges();
    Task<int> SaveChangesAsync();
}