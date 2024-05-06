using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Domain.FriendsInviteAggregate;
using Scheduler.Domain.UserAggregate;
using Scheduler.Domain.UserAggregate.DomainEvents;

namespace Scheduler.Application.Users.Event;

public class UserAcceptedFriendInviteEventHandler(IUsersRepository usersRepository, IFriendsInviteRepository friendsInviteRepository) : INotificationHandler<UserAcceptedFriendInviteEvent>
{
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly IFriendsInviteRepository _friendsInviteRepository = friendsInviteRepository;

    public Task Handle(UserAcceptedFriendInviteEvent notification, CancellationToken cancellationToken)
    {
        User sender = _usersRepository.GetUserById(notification.SenderId)
            ?? throw new NullReferenceException($"User with id {notification.SenderId.Value} not found.");
        FriendsInvite? friendsInvite = _friendsInviteRepository.GetFriendsInviteById(notification.FriendsInviteId)
            ?? throw new NullReferenceException($"No friends invite with id {notification.FriendsInviteId} found.");
        sender.SendedFriendInviteAcceptedCallback(friendsInvite);
        return Task.CompletedTask;
    }
}
