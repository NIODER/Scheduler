using Scheduler.Domain.Common;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.TaskAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Domain.TaskAggregate;

public class Task : Aggregate<TaskId>
{
    public UserId CreatorId { get; private set; }
    public UserId UserId { get; private set; }
    public GroupId GroupId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public TaskStatus Status { get; private set; }
    public DateTime Deadline { get; private set; }

    private Task()
    {
        CreatorId = default!;
        UserId = default!;
        GroupId = default!;
        Title = null!;
        Description = null!;
    }

    private Task(
        TaskId taskId,
        UserId creatorId,
        UserId userId,
        GroupId groupId,
        string title,
        string description,
        TaskStatus status,
        DateTime deadline
    ) : base(taskId)
    {
        CreatorId = creatorId;
        UserId = userId;
        GroupId = groupId;
        Title = title;
        Description = description;
        Status = status;
        Deadline = deadline;
    }

    public static Task Create(
        UserId creatorId,
        UserId userId,
        GroupId groupId,
        string title,
        string description,
        DateTime deadline,
        TaskStatus status = TaskStatus.New
    ) => new(
        TaskId.CreateUnique(),
        creatorId,
        userId,
        groupId,
        title,
        description,
        status,
        deadline
    );

}