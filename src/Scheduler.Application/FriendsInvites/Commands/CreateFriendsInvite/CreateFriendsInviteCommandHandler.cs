using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.FriendsInvites.Common;
using Scheduler.Domain.UserAggregate;

namespace Scheduler.Application.FriendsInvites.Commands.CreateFriendsInvite;

public class CreateFriendsInviteCommandHandler(IUsersRepository usersRepository)
    : IRequestHandler<CreateFriendsInviteCommand, FriendsInviteResult>
{
    private readonly IUsersRepository _usersRepository = usersRepository;

    public Task<FriendsInviteResult> Handle(CreateFriendsInviteCommand request, CancellationToken cancellationToken)
    {
        User sender = _usersRepository.GetUserById(new(request.SenderId))
            ?? throw new NullReferenceException($"No user with id {request.SenderId} not found.");
        User addressie = _usersRepository.GetUserById(new(request.AddressieId))
            ?? throw new NullReferenceException($"No user with id {request.AddressieId} not found.");
        var invite = sender.SendFriendsInvite(addressie, request.Message);
        var result = new FriendsInviteResult(
            invite.Id,
            invite.SenderId,
            invite.AddressieId,
            invite.Message
        );
        return Task.FromResult(result);
    }
}
