using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Domain.FinancialPlanAggregate;
using Scheduler.Domain.FinancialPlanAggregate.ValueObjects;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Infrastructure.Persistance.Repositories;

public sealed class FinancialPlansRepository(SchedulerDbContext context) : IFinancialPlansRepository
{
    public void Add(FinancialPlan financialPlan)
    {
        context.FinancialPlans.Add(financialPlan);
    }

    public void DeleteFinancialPlanById(FinancialPlanId financialPlanId)
    {
        var financialPlan = context.FinancialPlans.SingleOrDefault(f => f.Id == financialPlanId);
        if (financialPlan is not null)
        {
            context.FinancialPlans.Remove(financialPlan);
        }
    }

    public FinancialPlan? GetFinancialPlanById(FinancialPlanId financialPlanId)
    {
        return context.FinancialPlans.SingleOrDefault(f => f.Id == financialPlanId);
    }

    public Task<FinancialPlan?> GetFinancialPlanByIdAsync(FinancialPlanId financialPlanId)
    {
        return context.FinancialPlans.SingleOrDefaultAsync(f => f.Id == financialPlanId);
    }

    public List<FinancialPlan> GetGroupFinancialPlansByGroupId(GroupId groupId)
    {
        return context.FinancialPlans.Where(fp => fp.GroupId == groupId).ToList();
    }

    public Task<List<FinancialPlan>> GetGroupFinancialPlansByGroupIdAsync(GroupId groupId)
    {
        return context.FinancialPlans.Where(fp => fp.GroupId == groupId).ToListAsync();
    }

    public List<FinancialPlan> GetPrivateFinancialPlansByUserId(UserId userId)
    {
        return context.FinancialPlans
            .Where(fp => fp.CreatorId == userId)
            .Where(fp => fp.GroupId.Value == default)
            .ToList();
    }

    public Task<List<FinancialPlan>> GetPrivateFinancialPlansByUserIdAsync(UserId userId)
    {
        return context.FinancialPlans
            .Where(fp => fp.CreatorId == userId)
            .Where(fp => fp.GroupId.Value == default)
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

    public void Update(FinancialPlan financialPlan)
    {
        context.Update(financialPlan);
    }
}
