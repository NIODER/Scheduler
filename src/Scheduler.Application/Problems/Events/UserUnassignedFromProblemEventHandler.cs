using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Domain.ProblemAggregate.Events;
using Scheduler.Domain.UserAggregate;

namespace Scheduler.Application.Problems.Events;

internal class UserUnassignedFromProblemEventHandler(IUsersRepository usersRepository)
    : INotificationHandler<UserUnAssignedFromProblemEvent>
{
    private readonly IUsersRepository _usersRepository = usersRepository;

    public async Task Handle(UserUnAssignedFromProblemEvent notification, CancellationToken cancellationToken)
    {
        User? assignedUser = await _usersRepository.GetUserByIdAsync(notification.UserId);
        if (assignedUser is null)
        {
            return;
        }
        assignedUser.RemoveProblem(notification.ProblemId);
        _usersRepository.Update(assignedUser);
        await _usersRepository.SaveChangesAsync();
    }
}
