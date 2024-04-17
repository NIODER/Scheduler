using Scheduler.Domain.FinancialPlanAggregate;
using Scheduler.Domain.FinancialPlanAggregate.ValueObjects;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Application.Common.Interfaces.Persistance;

public interface IFinancialPlansRepository
{
    void Add(FinancialPlan financialPlan);
    void Update(FinancialPlan financialPlan);
    FinancialPlan? GetFinancialPlanById(FinancialPlanId financialPlanId);
    Task<FinancialPlan?> GetFinancialPlanByIdAsync(FinancialPlanId financialPlanId);
    void DeleteFinancialPlanById(FinancialPlanId financialPlanId);
    List<FinancialPlan> GetPrivateFinancialPlansByUserId(UserId userId);
    Task<List<FinancialPlan>> GetPrivateFinancialPlansByUserIdAsync(UserId userId);
    List<FinancialPlan> GetGroupFinancialPlansByGroupId(GroupId groupId);
    Task<List<FinancialPlan>> GetGroupFinancialPlansByGroupIdAsync(GroupId groupId);
    int SaveChanges();
    Task<int> SaveChangesAsync();
}