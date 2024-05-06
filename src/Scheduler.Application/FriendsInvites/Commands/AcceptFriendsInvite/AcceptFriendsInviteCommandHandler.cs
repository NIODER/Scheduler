using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.FriendsInvites.Common;
using Scheduler.Domain.FriendsInviteAggregate;
using Scheduler.Domain.UserAggregate;

namespace Scheduler.Application.FriendsInvites.Commands.AcceptFriendsInvite;

public class AcceptFriendsInviteCommandHandler(IUsersRepository usersRepository, IFriendsInviteRepository friendsInviteRepository)
    : IRequestHandler<AcceptFriendsInviteCommand, FriendsInviteResult>
{
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly IFriendsInviteRepository _friendsInviteRepository = friendsInviteRepository;

    public Task<FriendsInviteResult> Handle(AcceptFriendsInviteCommand request, CancellationToken cancellationToken)
    {
        User user = _usersRepository.GetUserById(new(request.ExecutorId))
            ?? throw new NullReferenceException($"No user with id {request.ExecutorId} found.");
        FriendsInvite friendsInvite = _friendsInviteRepository.GetFriendsInviteById(new(request.InviteId))
            ?? throw new NullReferenceException($"No friends invite with id {request.InviteId} found.");
        user.AcceptFriendshipInvite(friendsInvite);
        return Task.FromResult(
                new FriendsInviteResult(
                    friendsInvite.Id,
                    friendsInvite.SenderId,
                    friendsInvite.AddressieId,
                    friendsInvite.Message
                )
        );
    }
}
