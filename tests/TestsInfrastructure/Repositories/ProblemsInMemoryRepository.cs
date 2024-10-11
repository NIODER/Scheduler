using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.ProblemAggregate;
using Scheduler.Domain.ProblemAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace TestsInfrastructure.Repositories;

internal class ProblemsInMemoryRepository : IProblemsRepository
{
    public void Add(Problem problem)
    {
        throw new NotImplementedException();
    }

    public void DeleteProblemById(ProblemId problemId)
    {
        throw new NotImplementedException();
    }

    public List<Problem> GetGroupProblemsByGroupId(GroupId groupId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Problem>> GetGroupProblemsByGroupIdAsync(GroupId groupId)
    {
        throw new NotImplementedException();
    }

    public Problem? GetProblemById(ProblemId problemId)
    {
        throw new NotImplementedException();
    }

    public Task<Problem?> GetProblemByIdAsync(ProblemId problemId)
    {
        throw new NotImplementedException();
    }

    public List<Problem> GetProblemsAssignedToUserByUserId(UserId userId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Problem>> GetProblemsAssignedToUserByUserIdAsync(UserId userId)
    {
        throw new NotImplementedException();
    }

    public List<Problem> GetProblemsCreatedOrAssignedToUserByUserId(UserId userId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Problem>> GetProblemsCreatedOrAssignedToUserByUserIdAsync(UserId userId)
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

    public void Update(Problem problem)
    {
        throw new NotImplementedException();
    }
}
