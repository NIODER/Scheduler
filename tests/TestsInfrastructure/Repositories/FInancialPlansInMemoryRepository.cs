using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Domain.FinancialPlanAggregate;
using Scheduler.Domain.FinancialPlanAggregate.ValueObjects;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace TestsInfrastructure.Repositories;

internal class FInancialPlansInMemoryRepository : IFinancialPlansRepository
{
    private readonly SchedulerContext _context;

    public void Add(FinancialPlan financialPlan)
    {
        throw new NotImplementedException();
    }

    public void DeleteFinancialPlanById(FinancialPlanId financialPlanId)
    {
        throw new NotImplementedException();
    }

    public FinancialPlan? GetFinancialPlanById(FinancialPlanId financialPlanId)
    {
        throw new NotImplementedException();
    }

    public Task<FinancialPlan?> GetFinancialPlanByIdAsync(FinancialPlanId financialPlanId)
    {
        throw new NotImplementedException();
    }

    public List<FinancialPlan> GetGroupFinancialPlansByGroupId(GroupId groupId)
    {
        throw new NotImplementedException();
    }

    public Task<List<FinancialPlan>> GetGroupFinancialPlansByGroupIdAsync(GroupId groupId)
    {
        throw new NotImplementedException();
    }

    public List<FinancialPlan> GetPrivateFinancialPlansByUserId(UserId userId)
    {
        throw new NotImplementedException();
    }

    public Task<List<FinancialPlan>> GetPrivateFinancialPlansByUserIdAsync(UserId userId)
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

    public void Update(FinancialPlan financialPlan)
    {
        throw new NotImplementedException();
    }
}
