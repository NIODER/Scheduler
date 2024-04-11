namespace Scheduler.Domain.UserAggregate;

[Flags]
public enum UserGroupPermissions
{
    CreateTask = 1 << 0,
    ChangeUnownedTask = 1 << 1,
    DeleteUnownedTask = 1 << 2,
    CreateInvite = 1 << 3,
    ChangeGroupSettings = 1 << 4,
    CreateFinancialPlan = 1 << 5,
    ChangeUnownedFinancialPlan = 1 << 6,
    DeleteUnownedFinancialPlan = 1 << 7
}