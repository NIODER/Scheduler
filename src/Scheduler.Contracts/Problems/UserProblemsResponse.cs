namespace Scheduler.Contracts.Problems;

public record UserProblemsResponse(
    int Count,
    Guid UserId,
    List<ProblemResponse> Tasks
);
