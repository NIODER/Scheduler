using Microsoft.EntityFrameworkCore;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Domain.FinancialPlanAggregate;
using Scheduler.Domain.FinancialPlanAggregate.ValueObjects;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Infrastructure.Persistance.Repositories;

public sealed class FinancialPlansRepository(SchedulerDbContext context) : IFinancialPlansRepository
{
    private const string INVELID_FINANCIALPLAN_ID_EXCEPTION_MESSAGE = "No financial plan with given id was found.";
    private const string INVALID_GROUP_ID_EXCEPTION_MESSAGE = "No group with given id was found";

    public void Add(FinancialPlan financialPlan)
    {
        context.FinancialPlans.Add(financialPlan);
    }

    public void DeleteFinancialPlanById(FinancialPlanId financialPlanId)
    {
        context.FinancialPlans.SingleOrDefault(f => f.Id == financialPlanId);
    }

    public FinancialPlan GetFinancialPlanById(FinancialPlanId financialPlanId)
    {
        return context.FinancialPlans.SingleOrDefault(f => f.Id == financialPlanId)
            ?? throw new NullReferenceException(INVELID_FINANCIALPLAN_ID_EXCEPTION_MESSAGE);
    }

    public async Task<FinancialPlan> GetFinancialPlanByIdAsync(FinancialPlanId financialPlanId)
    {
        return await context.FinancialPlans.SingleOrDefaultAsync(f => f.Id == financialPlanId)
            ?? throw new NullReferenceException(INVELID_FINANCIALPLAN_ID_EXCEPTION_MESSAGE);
    }

    public IEnumerable<FinancialPlan> GetGroupFinancialPlansByGroupId(GroupId groupId)
    {
        var financialPlanIds = context.Groups
            .Include(g => g.FinancialPlanIds)
            .SingleOrDefault(g => g.Id == groupId)
            ?.FinancialPlanIds
            ?? throw new NullReferenceException(INVALID_GROUP_ID_EXCEPTION_MESSAGE);
        return context.FinancialPlans.Where(f => financialPlanIds.Contains(f.Id))
            .AsEnumerable();
    }

    public async IAsyncEnumerable<FinancialPlan> GetGroupFinancialPlansByGroupIdAsync(GroupId groupId)
    {
        var group = await context.Groups
            .Include(g => g.FinancialPlanIds)
            .SingleOrDefaultAsync(g => g.Id == groupId)
            ?? throw new NullReferenceException(INVALID_GROUP_ID_EXCEPTION_MESSAGE);

        var financialPlans = context.FinancialPlans.Where(f => group.FinancialPlanIds.Contains(f.Id))
            .AsAsyncEnumerable();
        await foreach (var financialPlan in financialPlans)
        {
            yield return financialPlan;
        }
    }

    public IEnumerable<FinancialPlan> GetPrivateFinancialPlansByUserId(UserId userId)
    {
        var user = context.Users
            .Include(u => u.FinancialPlanIds)
            .SingleOrDefault(u => u.Id == userId)
            ?? throw new NullReferenceException(INVALID_GROUP_ID_EXCEPTION_MESSAGE);
        return context.FinancialPlans.Where(f => user.FinancialPlanIds.Contains(f.Id))
            .AsEnumerable();
    }

    public async IAsyncEnumerable<FinancialPlan> GetPrivateFinancialPlansByUserIdAsync(UserId userId)
    {
        var user = await context.Users
            .Include(u => u.FinancialPlanIds)
            .SingleOrDefaultAsync(u => u.Id == userId)
            ?? throw new NullReferenceException(INVALID_GROUP_ID_EXCEPTION_MESSAGE);
        var financialPlans = context.FinancialPlans.Where(f => user.FinancialPlanIds.Contains(f.Id))
            .AsAsyncEnumerable();
        await foreach (var financialPlan in financialPlans)
        {
            yield return financialPlan;
        }
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
