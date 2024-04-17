using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.ProblemAggregate;
using Scheduler.Domain.ProblemAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Application.Common.Interfaces.Persistance;

public interface IProblemsRepository
{
    void Add(Problem problem);
    void Update(Problem problem);
    Problem GetProblemById(ProblemId problemId);
    Task<Problem> GetProblemByIdAsync(ProblemId problemId);
    IEnumerable<Problem> GetProblemsCreatedOrAssignedToUserByUserId(UserId userId);
    IAsyncEnumerable<Problem> GetProblemsCreatedOrAssignedToUserByUserIdAsync(UserId userId);
    IEnumerable<Problem> GetProblemsAssignedToUserByUserId(UserId userId);
    IAsyncEnumerable<Problem> GetProblemsAssignedToUserByUserIdAsync(UserId userId);
    IEnumerable<Problem> GetGroupProblemsByGroupId(GroupId groupId);
    IAsyncEnumerable<Problem> GetGroupProblemsByGroupIdAsync(GroupId groupId);
    void DeleteProblemById(ProblemId problemId);
    int SaveChanges();
    Task<int> SaveChangesAsync();
}