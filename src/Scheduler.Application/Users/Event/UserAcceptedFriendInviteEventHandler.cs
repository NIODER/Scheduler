using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Domain.UserAggregate;
using Scheduler.Domain.UserAggregate.DomainEvents;

namespace Scheduler.Application.Users.Event;

public class UserAcceptedFriendInviteEventHandler(IUsersRepository usersRepository) : INotificationHandler<UserAcceptedFriendInviteEvent>
{
    private readonly IUsersRepository _usersRepository = usersRepository;

    public async Task Handle(UserAcceptedFriendInviteEvent notification, CancellationToken cancellationToken)
    {
        User sender = _usersRepository.GetUserById(notification.SenderId)
            ?? throw new NullReferenceException($"User with id {notification.SenderId.Value} not found.");
        sender.AddFriend(notification.AddressieId);
        _usersRepository.Update(sender);
        await _usersRepository.SaveChangesAsync();
    }
}
