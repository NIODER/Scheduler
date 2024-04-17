using Scheduler.Domain.FinancialPlanAggregate;
using Scheduler.Domain.FinancialPlanAggregate.ValueObjects;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Application.Common.Interfaces.Persistance;

public interface IFinancialPlansRepository
{
    void Add(FinancialPlan financialPlan);
    void Update(FinancialPlan financialPlan);
    FinancialPlan GetFinancialPlanById(FinancialPlanId financialPlanId);
    Task<FinancialPlan> GetFinancialPlanByIdAsync(FinancialPlanId financialPlanId);
    void DeleteFinancialPlanById(FinancialPlanId financialPlanId);
    IEnumerable<FinancialPlan> GetPrivateFinancialPlansByUserId(UserId userId);
    IAsyncEnumerable<FinancialPlan> GetPrivateFinancialPlansByUserIdAsync(UserId userId);
    IEnumerable<FinancialPlan> GetGroupFinancialPlansByGroupId(GroupId groupId);
    IAsyncEnumerable<FinancialPlan> GetGroupFinancialPlansByGroupIdAsync(GroupId groupId);
    int SaveChanges();
    Task<int> SaveChangesAsync();
}