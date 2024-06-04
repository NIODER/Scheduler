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
    public int? Status { get; set; }
    public DateTime Deadline { get; set; }

    public ProblemRequest(
        Guid? userId,
        Guid? groupId,
        string title,
        string description,
        int? status,
        DateTime deadline)
    {
        AssignedUserId = userId;
        GroupId = groupId;
        Title = title;
        Description = description;
        Status = status;
        Deadline = deadline;
    }
}