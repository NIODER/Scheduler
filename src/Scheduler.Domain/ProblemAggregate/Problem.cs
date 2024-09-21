using Scheduler.Domain.Common;
using Scheduler.Domain.GroupAggregate;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.ProblemAggregate.Events;
using Scheduler.Domain.ProblemAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Domain.ProblemAggregate;

public class Problem : Aggregate<ProblemId>
{
    public UserId CreatorId { get; private set; }
    public UserId? UserId { get; private set; }
    public GroupId? GroupId { get; private set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public ProblemStatus Status { get; set; }
    public DateTime Deadline { get; set; }

    public bool IsAssignedToGroup => GroupId is not null;

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

    public static Problem CreatePrivate(
        UserId creatorId,
        string title,
        string description,
        DateTime deadline,
        ProblemStatus status = ProblemStatus.New
    ) => new(
        new(Guid.NewGuid()),
        creatorId,
        creatorId,
        null,
        title,
        description,
        status,
        deadline
    );

    /// <exception cref="InvalidOperationException"></exception>
    public void UnassignUserFromProblem()
    {
        if (GroupId is null)
        {
            throw new InvalidOperationException("Can't unassign user from private task.");
        }
        if (UserId is not null)
        {
            AddDomainEvent(new UserUnAssignedFromProblemEvent(Id, UserId));
            UserId = null;
        }
    }

    /// <exception cref="InvalidOperationException"></exception>
    public void AssignUserToProblem(UserId userId)
    {
        if (GroupId is null && userId != UserId)
        {
            throw new InvalidOperationException("Can't change user assigned to private task.");
        }
        UserId = userId;
        AddDomainEvent(new UserAssignedToProblemEvent(Id, userId));
    }

    public void Delete(UserId executorId, Group? group = null)
    {
        if (IsAssignedToGroup)
        {
            if (group is null)
            {
                throw new ArgumentNullException(nameof(group), "Impossible to detemine user access permissions due to group is not specified.");
            }
            if (!group.ProblemIds.Contains(Id))
            {
                return;
            }
            if (group.Id != GroupId)
            {
                throw new ArgumentException($"{nameof(GroupId)} and {nameof(group.Id)} is not equal.", nameof(group));
            }
            if (!group.UserHasPermissions(executorId, UserGroupPermissions.DeleteTask))
            {
                throw new InvalidOperationException("User has no permissions to delete tasks in this group.");
            }
        }
        else if (CreatorId != executorId)
        {
            throw new InvalidOperationException("User has no permissions to delete this task.");
        }
        AddDomainEvent(new ProblemDeletedEvent(Id));
    }
}