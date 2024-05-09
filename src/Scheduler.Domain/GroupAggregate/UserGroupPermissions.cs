namespace Scheduler.Domain.GroupAggregate;

[Flags]
public enum UserGroupPermissions
{
    CreateTask = 1 << 0,
    ChangeUnownedTask = 1 << 1,
    DeleteUnownedTask = 1 << 2,
    CreateInvite = 1 << 3,
    DeleteInvites = 1 << 4,
    ChangeGroupSettings = 1 << 5,
    CreateFinancialPlan = 1 << 6,
    ChangeUnownedFinancialPlan = 1 << 7,
    DeleteUnownedFinancialPlan = 1 << 8,
    IsGroupOwner = 1 << 9,
    ChangeGroupUserSettings = 1 << 10,
    DeleteGroupUser = 1 << 11,
    All = ~(0)
}