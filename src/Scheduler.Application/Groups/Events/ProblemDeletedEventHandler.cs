using MediatR;
using Microsoft.Extensions.Logging;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Domain.ProblemAggregate.Events;

namespace Scheduler.Application.Groups.Events;

internal class ProblemDeletedEventHandler(
    IGroupsRepository groupsRepository, 
    IProblemsRepository problemsRepository, 
    ILogger<ProblemDeletedEventHandler> logger) : INotificationHandler<ProblemDeletedEvent>
{
    private readonly IGroupsRepository _groupsRepository = groupsRepository;
    private readonly IProblemsRepository _problemsRepository = problemsRepository;
    private readonly ILogger<ProblemDeletedEventHandler> _logger = logger;

    public async Task Handle(ProblemDeletedEvent notification, CancellationToken cancellationToken)
    {
        var problem = await _problemsRepository.GetProblemByIdAsync(notification.ProblemId);
        if (problem is null)
        {
            return;
        }
        if (problem.GroupId is null)
        {
            return;
        }
        var group = await _groupsRepository.GetGroupByIdAsync(problem.GroupId);
        if (group is null)
        {
            return;
        }
        group.RemoveProblem(problem.Id);
        _groupsRepository.Update(group);
        _logger.LogInformation("Problem {problemId} deleted from group {groupId}.", problem.Id, group.Id);
        await _groupsRepository.SaveChangesAsync();
    }
}
