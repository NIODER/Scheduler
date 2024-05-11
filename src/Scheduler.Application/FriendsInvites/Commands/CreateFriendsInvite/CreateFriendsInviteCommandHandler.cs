using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.FriendsInvites.Common;
using Scheduler.Domain.UserAggregate;

namespace Scheduler.Application.FriendsInvites.Commands.CreateFriendsInvite;

public class CreateFriendsInviteCommandHandler(IUsersRepository usersRepository)
    : IRequestHandler<CreateFriendsInviteCommand, ICommandResult<FriendsInviteResult>>
{
    private readonly IUsersRepository _usersRepository = usersRepository;

    public async Task<ICommandResult<FriendsInviteResult>> Handle(CreateFriendsInviteCommand request, CancellationToken cancellationToken)
    {
        User? sender = await _usersRepository.GetUserByIdAsync(new(request.SenderId));
        if (sender is null)
        {
            return new NotFound<FriendsInviteResult>($"No user with id {request.SenderId} not found.");
        }
        User? addressie = await _usersRepository.GetUserByIdAsync(new(request.AddressieId));
        if (addressie is null)
        {
            return new NotFound<FriendsInviteResult>($"No user with id {request.SenderId} not found.");
        }
        if (sender.FriendsIds.Any(f => f == addressie.Id))
        {
            return new ExpectedErrorResult<FriendsInviteResult>($"User with id {request.AddressieId} is already a friend.");
        }
        var invite = sender.SendFriendsInvite(addressie, request.Message);
        _usersRepository.Update(sender);
        await _usersRepository.SaveChangesAsync();
        var result = new FriendsInviteResult(invite.Id, invite.SenderId, invite.AddressieId, invite.Message);
        return new SuccessResult<FriendsInviteResult>(result);
    }
}
