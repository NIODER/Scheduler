using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.FriendsInvites.Common;
using Scheduler.Domain.FriendsInviteAggregate;
using Scheduler.Domain.UserAggregate;

namespace Scheduler.Application.FriendsInvites.Commands.CreateFriendsInvite;

public class CreateFriendsInviteCommandHandler(IUsersRepository usersRepository, IFriendsInviteRepository friendsInviteRepository)
    : IRequestHandler<CreateFriendsInviteCommand, FriendsInviteResult>
{
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly IFriendsInviteRepository _friendsInviteRepository = friendsInviteRepository;

    public Task<FriendsInviteResult> Handle(CreateFriendsInviteCommand request, CancellationToken cancellationToken)
    {
        User sender = _usersRepository.GetUserById(new(request.SenderId))
            ?? throw new NullReferenceException($"No user with id {request.SenderId} not found.");
        User addressie = _usersRepository.GetUserById(new(request.AddressieId))
            ?? throw new NullReferenceException($"No user with id {request.AddressieId} not found.");
        var invite = FriendsInvite.CreateWithFriendsInviteCreatedEvent(
            senderId: sender.Id,
            addressieId: addressie.Id,
            request.Message
        );
        _friendsInviteRepository.CreateFriendsInvite(invite);
        _friendsInviteRepository.SaveChanges();
        var result = new FriendsInviteResult(
            invite.Id,
            invite.SenderId,
            invite.AddressieId,
            invite.Message
        );
        return Task.FromResult(result);
    }
}
