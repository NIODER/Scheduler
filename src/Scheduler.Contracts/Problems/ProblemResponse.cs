using System.Text.Json.Serialization;

namespace Scheduler.Contracts.Problems;

public record ProblemResponse
{
    public Guid TaskId { get; set; }
    public Guid CreatorId { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Guid? UserId { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Guid? GroupId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime Deadline { get; set; }

    public ProblemResponse(
        Guid taskId,
        Guid creatorId,
        Guid? userId,
        Guid? groupId,
        string title,
        string description,
        string status,
        DateTime deadline)
    {
        TaskId = taskId;
        CreatorId = creatorId;
        UserId = userId;
        GroupId = groupId;
        Title = title;
        Description = description;
        Status = status;
        Deadline = deadline;
    }
}
