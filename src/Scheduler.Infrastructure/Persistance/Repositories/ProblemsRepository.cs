using Microsoft.EntityFrameworkCore;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.ProblemAggregate;
using Scheduler.Domain.ProblemAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Infrastructure.Persistance.Repositories;

public sealed class ProblemsRepository(SchedulerDbContext context) : IProblemsRepository
{
    public void Add(Problem problem)
    {
        context.Problems.Add(problem);
    }

    public void DeleteProblemById(ProblemId problemId)
    {
        var problem = context.Problems.SingleOrDefault(p => p.Id == problemId);
        if (problem is not null)
        {
            context.Problems.Remove(problem);
        }
    }

    public List<Problem> GetGroupProblemsByGroupId(GroupId groupId)
    {
        return context.Problems.Where(p => p.GroupId == groupId)
            .ToList();
    }

    public Task<List<Problem>> GetGroupProblemsByGroupIdAsync(GroupId groupId)
    {
        return context.Problems.Where(p => p.GroupId == groupId)
            .ToListAsync();
    }

    public Problem? GetProblemById(ProblemId problemId)
    {
        return context.Problems.SingleOrDefault(p => p.Id == problemId);
    }

    public Task<Problem?> GetProblemByIdAsync(ProblemId problemId)
    {
        return context.Problems.SingleOrDefaultAsync(p => p.Id == problemId);
    }

    public List<Problem> GetProblemsAssignedToUserByUserId(UserId userId)
    {
        return context.Problems.Where(p => p.UserId == userId)
            .ToList();
    }

    public Task<List<Problem>> GetProblemsAssignedToUserByUserIdAsync(UserId userId)
    {
        return context.Problems.Where(p => p.UserId == userId)
            .ToListAsync();
    }

    public List<Problem> GetProblemsCreatedOrAssignedToUserByUserId(UserId userId)
    {
        return context.Problems.Where(p => p.UserId == userId || p.CreatorId == userId)
            .ToList();
    }

    public Task<List<Problem>> GetProblemsCreatedOrAssignedToUserByUserIdAsync(UserId userId)
    {
        return context.Problems.Where(p => p.UserId == userId || p.CreatorId == userId)
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

    public void Update(Problem problem)
    {
        context.Problems.Update(problem);
    }
}
