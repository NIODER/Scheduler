using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.FriendsInvites.Common;
using Scheduler.Domain.FriendsInviteAggregate;
using Scheduler.Domain.UserAggregate;

namespace Scheduler.Application.FriendsInvites.Commands.DeleteFriendsInvite;

public class DeleteFriendsInviteCommandHandler(IUsersRepository usersRepository, IFriendsInviteRepository friendsInviteRepository)
        : IRequestHandler<DeleteFriendsInviteCommand, FriendsInviteResult>
{
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly IFriendsInviteRepository _friendsInviteRepository = friendsInviteRepository;

    public Task<FriendsInviteResult> Handle(DeleteFriendsInviteCommand request, CancellationToken cancellationToken)
    {
        User sender = _usersRepository.GetUserById(new(request.SenderId))
            ?? throw new NullReferenceException($"No user with id {request.SenderId} found.");
        FriendsInvite friendsInvite = _friendsInviteRepository.GetFriendsInviteById(new(request.InviteId))
            ?? throw new NullReferenceException($"No friends invite with id {request.InviteId} was found");
        sender.DeleteFriendsInvite(friendsInvite.Id);
        _usersRepository.Update(sender);
        _usersRepository.SaveChanges();
        var result = new FriendsInviteResult(
            friendsInvite.Id,
            friendsInvite.SenderId,
            friendsInvite.AddressieId,
            friendsInvite.Message
        );
        return Task.FromResult(result);
    }
}
