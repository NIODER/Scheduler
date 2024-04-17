using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.ProblemAggregate;
using Scheduler.Domain.ProblemAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Application.Common.Interfaces.Persistance;

public interface IProblemsRepository
{
    void Add(Problem problem);
    void Update(Problem problem);
    Problem? GetProblemById(ProblemId problemId);
    Task<Problem?> GetProblemByIdAsync(ProblemId problemId);
    List<Problem> GetProblemsCreatedOrAssignedToUserByUserId(UserId userId);
    Task<List<Problem>> GetProblemsCreatedOrAssignedToUserByUserIdAsync(UserId userId);
    List<Problem> GetProblemsAssignedToUserByUserId(UserId userId);
    Task<List<Problem>> GetProblemsAssignedToUserByUserIdAsync(UserId userId);
    List<Problem> GetGroupProblemsByGroupId(GroupId groupId);
    Task<List<Problem>> GetGroupProblemsByGroupIdAsync(GroupId groupId);
    void DeleteProblemById(ProblemId problemId);
    int SaveChanges();
    Task<int> SaveChangesAsync();
}