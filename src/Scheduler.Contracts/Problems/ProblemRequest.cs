using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Scheduler.Contracts.Problems;

public record ProblemRequest
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Guid? AssignedUserId { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Guid? GroupId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime Deadline { get; set; }

    public ProblemRequest(
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
        AssignedUserId = userId;
        GroupId = groupId;
        Title = title;
        Description = description;
        Status = status;
        Deadline = deadline;
    }
}