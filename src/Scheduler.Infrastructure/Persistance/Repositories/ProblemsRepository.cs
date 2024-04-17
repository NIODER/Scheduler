using Microsoft.EntityFrameworkCore;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.ProblemAggregate;
using Scheduler.Domain.ProblemAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Infrastructure.Persistance.Repositories;

public sealed class ProblemsRepository(SchedulerDbContext context) : IProblemsRepository
{
    private const string INVALID_PROBLEM_ID_EXCEPTION_MESSAGE = "No problem with given id found.";
    public void Add(Problem problem)
    {
        context.Problems.Add(problem);
    }

    public void DeleteProblemById(ProblemId problemId)
    {
        var problem = context.Problems.SingleOrDefault(p => p.Id == problemId)
            ?? throw new NullReferenceException(INVALID_PROBLEM_ID_EXCEPTION_MESSAGE);
        context.Problems.Remove(problem);
    }

    public IEnumerable<Problem> GetGroupProblemsByGroupId(GroupId groupId)
    {
        return context.Problems.Where(p => p.GroupId == groupId)
            .AsEnumerable();
    }

    public IAsyncEnumerable<Problem> GetGroupProblemsByGroupIdAsync(GroupId groupId)
    {
        return context.Problems.Where(p => p.GroupId == groupId)
            .AsAsyncEnumerable();
    }

    public Problem GetProblemById(ProblemId problemId)
    {
        return context.Problems.SingleOrDefault(p => p.Id == problemId)
            ?? throw new NullReferenceException(INVALID_PROBLEM_ID_EXCEPTION_MESSAGE);
    }

    public async Task<Problem> GetProblemByIdAsync(ProblemId problemId)
    {
        return await context.Problems.SingleOrDefaultAsync(p => p.Id == problemId)
            ?? throw new NullReferenceException(INVALID_PROBLEM_ID_EXCEPTION_MESSAGE);
    }

    public IEnumerable<Problem> GetProblemsAssignedToUserByUserId(UserId userId)
    {
        return context.Problems.Where(p => p.UserId == userId)
            .AsEnumerable();
    }

    public IAsyncEnumerable<Problem> GetProblemsAssignedToUserByUserIdAsync(UserId userId)
    {
        return context.Problems.Where(p => p.UserId == userId)
            .AsAsyncEnumerable();
    }

    public IEnumerable<Problem> GetProblemsCreatedOrAssignedToUserByUserId(UserId userId)
    {
        return context.Problems.Where(p => p.UserId == userId || p.CreatorId == userId)
            .AsEnumerable();
    }

    public IAsyncEnumerable<Problem> GetProblemsCreatedOrAssignedToUserByUserIdAsync(UserId userId)
    {
        return context.Problems.Where(p => p.UserId == userId || p.CreatorId == userId)
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

    public void Update(Problem problem)
    {
        context.Problems.Update(problem);
    }
}
