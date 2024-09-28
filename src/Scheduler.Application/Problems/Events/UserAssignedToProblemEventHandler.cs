using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Domain.ProblemAggregate.Events;
using Scheduler.Domain.UserAggregate;

namespace Scheduler.Application.Problems.Events;

internal class UserAssignedToProblemEventHandler(IUsersRepository usersRepository) : INotificationHandler<UserAssignedToProblemEvent>
{
    private readonly IUsersRepository _usersRepository = usersRepository;

    public async Task Handle(UserAssignedToProblemEvent notification, CancellationToken cancellationToken)
    {
        User? assignedUser = await _usersRepository.GetUserByIdAsync(notification.UserId);
        if (assignedUser is null)
        {
            return;
        }
        assignedUser.AddProblem(notification.ProblemId);
        _usersRepository.Update(assignedUser);
        await _usersRepository.SaveChangesAsync();
    }
}
