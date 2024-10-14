using Scheduler.Domain.Common;
using Scheduler.Domain.FinancialPlanAggregate.ValueObjects;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Domain.FinancialPlanAggregate;

public class FinancialPlan : Aggregate<FinancialPlanId>
{
    private readonly List<Charge> _charges = [];
    public string Title { get; set; }
    public UserId CreatorId { get; private set; }
    public GroupId? GroupId { get; set; }

    private FinancialPlan()
    {
        Title = null!;
        CreatorId = default!;
        GroupId = default!;
    }

    private FinancialPlan(FinancialPlanId id, string title, UserId creatorId, GroupId? groupId, List<Charge> charges) : base(id)
    {
        Title = title;
        _charges = charges;
        CreatorId = creatorId;
        GroupId = groupId;
    }

    public static FinancialPlan CreatePrivate(
        string title,
        UserId creatorId,
        List<Charge> charges
    ) => new(new FinancialPlanId(Guid.NewGuid()), title, creatorId, null, charges);

    public static FinancialPlan CreateGroup(
        string title,
        UserId creatorId,
        GroupId groupId,
        List<Charge> charges
    ) => new(new FinancialPlanId(Guid.NewGuid()), title, creatorId, groupId, charges);

    public IReadOnlyCollection<Charge> Charges => _charges.AsReadOnly();

    public bool IsPrivate => GroupId is null;
}