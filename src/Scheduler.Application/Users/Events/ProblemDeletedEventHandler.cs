using MediatR;
using Microsoft.Extensions.Logging;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Domain.ProblemAggregate.Events;

namespace Scheduler.Application.Users.Events;

internal class ProblemDeletedEventHandler(
    IUsersRepository usersRepository, 
    IProblemsRepository problemsRepository, 
    ILogger<ProblemDeletedEventHandler> logger) : INotificationHandler<ProblemDeletedEvent>
{
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly IProblemsRepository _problemsRepository = problemsRepository;
    private readonly ILogger<ProblemDeletedEventHandler> _logger = logger;

    public async Task Handle(ProblemDeletedEvent notification, CancellationToken cancellationToken)
    {
        var problem = await _problemsRepository.GetProblemByIdAsync(notification.ProblemId);
        if (problem is null)
        {
            return;
        }
        if (problem.UserId is not null)
        {
            var assignedUser = await _usersRepository.GetUserByIdAsync(problem.UserId);
            if (assignedUser is not null)
            {
                assignedUser.RemoveProblem(problem.Id);
                _usersRepository.Update(assignedUser);
                _logger.LogInformation("Remove task {problemId} from user {assignedUserId}", problem.Id, assignedUser.Id);
            }
        }
        var creator = await _usersRepository.GetUserByIdAsync(problem.CreatorId);
        if (creator is not null)
        {
            creator.RemoveProblem(problem.Id);
            _usersRepository.Update(creator);
            _logger.LogInformation("Remove task {problemId} from user {creatorId}", problem.Id, creator.Id);
        }
        await _usersRepository.SaveChangesAsync();
    }
}
