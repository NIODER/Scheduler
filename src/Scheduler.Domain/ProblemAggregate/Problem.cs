using Scheduler.Domain.Common;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.ProblemAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Domain.ProblemAggregate;

public class Problem : Aggregate<ProblemId>
{
    public UserId CreatorId { get; private set; }
    public UserId? UserId { get; private set; }
    public GroupId? GroupId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public ProblemStatus Status { get; private set; }
    public DateTime Deadline { get; private set; }

    private Problem()
    {
        CreatorId = default!;
        Title = null!;
        Description = null!;
    }

    private Problem(
        ProblemId problemId,
        UserId creatorId,
        UserId? userId,
        GroupId? groupId,
        string title,
        string description,
        ProblemStatus status,
        DateTime deadline
    ) : base(problemId)
    {
        CreatorId = creatorId;
        UserId = userId;
        GroupId = groupId;
        Title = title;
        Description = description;
        Status = status;
        Deadline = deadline;
    }

    public static Problem Create(
        UserId creatorId,
        UserId? userId,
        GroupId? groupId,
        string title,
        string description,
        DateTime deadline,
        ProblemStatus status = ProblemStatus.New
    ) => new(
        new(Guid.NewGuid()),
        creatorId,
        userId,
        groupId,
        title,
        description,
        status,
        deadline
    );

}