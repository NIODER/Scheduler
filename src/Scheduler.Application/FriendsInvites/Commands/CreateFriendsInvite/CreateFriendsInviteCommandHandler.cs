using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.FriendsInvites.Common;
using Scheduler.Domain.UserAggregate;

namespace Scheduler.Application.FriendsInvites.Commands.CreateFriendsInvite;

public class CreateFriendsInviteCommandHandler(IUsersRepository usersRepository)
    : IRequestHandler<CreateFriendsInviteCommand, FriendsInviteResult>
{
    private readonly IUsersRepository _usersRepository = usersRepository;

    public async Task<FriendsInviteResult> Handle(CreateFriendsInviteCommand request, CancellationToken cancellationToken)
    {
        User sender = await _usersRepository.GetUserByIdAsync(new(request.SenderId))
            ?? throw new NullReferenceException($"No user with id {request.SenderId} not found.");
        User addressie = await _usersRepository.GetUserByIdAsync(new(request.AddressieId))
            ?? throw new NullReferenceException($"No user with id {request.AddressieId} not found.");
        if (sender.FriendsIds.Any(f => f == addressie.Id))
        {
            throw new Exception($"User is already a friend.");
        }
        var invite = sender.SendFriendsInvite(addressie, request.Message);
        var result = new FriendsInviteResult(
            invite.Id,
            invite.SenderId,
            invite.AddressieId,
            invite.Message
        );
        _usersRepository.Update(sender);
        await _usersRepository.SaveChangesAsync();
        return result;
    }
}
