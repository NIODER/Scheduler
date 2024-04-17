using Scheduler.Domain.GroupAggregate;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Application.Common.Interfaces.Persistance;

public interface IGroupsRepository
{
    void Add(Group group);
    void Update(Group group);
    Group? GetGroupById(GroupId groupId);
    Task<Group?> GetGroupByIdAsync(GroupId groupId);
    List<Group> GetGroupsByUserId(UserId userId);
    Task<List<Group>> GetGroupsByUserIdAsync(UserId userId);
    void DeleteGroupById(GroupId groupId);
    int SaveChanges();
    Task<int> SaveChangesAsync();
}