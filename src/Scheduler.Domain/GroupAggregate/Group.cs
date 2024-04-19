using Scheduler.Domain.Common;
using Scheduler.Domain.FinancialPlanAggregate.ValueObjects;
using Scheduler.Domain.GroupAggregate.Entities;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.ProblemAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Domain.GroupAggregate;

public class Group : Aggregate<GroupId>
{
    private readonly List<GroupUser> _users = [];
    private readonly List<GroupInvite> _invites = [];
    private readonly List<ProblemId> _problemIds = [];
    private readonly List<FinancialPlanId> _financialPlanIds = [];
    public string GroupName { get; set; }


    private Group()
    {
        GroupName = default!;
    }

    private Group(
        GroupId groupId,
        string groupName,
        List<GroupUser> users,
        List<GroupInvite> invites,
        List<ProblemId> problemIds,
        List<FinancialPlanId> financialPlanIds
    ) : base(groupId)
    {
        GroupName = groupName;
        _users = users;
        _invites = invites;
        _problemIds = problemIds;
        _financialPlanIds = financialPlanIds;
    }

    public static Group Create(string groupName) => new(
        new GroupId(Guid.NewGuid()),
        groupName, 
        users: [],
        invites: [],
        problemIds: [],
        financialPlanIds: []);

    public IReadOnlyCollection<GroupUser> Users => _users.AsReadOnly();
    public IReadOnlyCollection<GroupInvite> Invites => _invites.AsReadOnly();
    public IReadOnlyCollection<ProblemId> ProblemIds => _problemIds.AsReadOnly();
    public IReadOnlyCollection<FinancialPlanId> FinancialPlanIds => _financialPlanIds.AsReadOnly();

    public bool UserHasPermissions(UserId userId, UserGroupPermissions permissions)
    {
        var user = _users.FirstOrDefault(x => x.UserId == userId);
        if (user is not null && user.Permissions.HasFlag(permissions))
        {
            return true;
        }
        return false;
    }
}