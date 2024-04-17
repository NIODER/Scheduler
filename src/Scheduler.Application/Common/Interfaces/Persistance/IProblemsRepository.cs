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
    IEnumerable<Problem> GetProblemsVisibleToUserByUserId(UserId userId);
    Task<IEnumerable<Problem>> GetProblemsVisibleToUserByUserIdAsync(UserId userId);
    IEnumerable<Problem> GetProblemsAssignedToUserByUserId(UserId userId);
    Task<IEnumerable<Problem>> GetProblemsAssignedToUserByUserIdAsync(UserId userId);
    IEnumerable<Problem> GetGroupFinancialPlansByGroupId(GroupId groupId);
    Task<IEnumerable<Problem>> GetGroupFinancialPlansByGroupIdAsync(GroupId groupId);
    void DeleteProblemById(ProblemId problemId);
    int SaveChanges();
    Task<int> SaveChangesAsync();
}