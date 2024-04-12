using Scheduler.Domain.Common;
using Scheduler.Domain.FinancialPlanAggregate.ValueObjects;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.GroupInviteAggregate.ValueObjects;
using Scheduler.Domain.TaskAggregate.ValueObjects;

namespace Scheduler.Domain.GroupAggregate;

public class Group : Aggregate<GroupId>
{
    private readonly List<GroupUser> _users = [];
    private readonly List<GroupInviteId> _inviteIds = [];
    private readonly List<TaskId> _taskIds = [];
    private readonly List<FinancialPlanId> _financialPlanIds = [];
    public string GroupName { get; private set; }


    private Group()
    {
        GroupName = default!;
    }

    private Group(
        GroupId groupId,
        string groupName,
        List<GroupUser> users,
        List<GroupInviteId> inviteIds,
        List<TaskId> taskIds,
        List<FinancialPlanId> financialPlanIds
    ) : base(groupId)
    {
        GroupName = groupName;
        _users = users;
        _inviteIds = inviteIds;
        _taskIds = taskIds;
        _financialPlanIds = financialPlanIds;
    }

    public static Group Create(string groupName) => new(
        GroupId.CreateUnique(),
        groupName, 
        users: [],
        inviteIds: [],
        taskIds: [],
        financialPlanIds: []);

    public IReadOnlyCollection<GroupUser> Users => _users.AsReadOnly();
    public IReadOnlyCollection<GroupInviteId> InviteIds => _inviteIds.AsReadOnly();
    public IReadOnlyCollection<TaskId> TaskIds => _taskIds.AsReadOnly();
    public IReadOnlyCollection<FinancialPlanId> FinancialPlanIds => _financialPlanIds.AsReadOnly();
}