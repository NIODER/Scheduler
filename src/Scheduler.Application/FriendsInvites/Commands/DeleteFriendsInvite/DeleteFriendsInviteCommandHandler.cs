using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.FriendsInvites.Common;
using Scheduler.Domain.UserAggregate;
using Scheduler.Domain.UserAggregate.Entities;

namespace Scheduler.Application.FriendsInvites.Commands.DeleteFriendsInvite;

public class DeleteFriendsInviteCommandHandler(IUsersRepository usersRepository)
    : IRequestHandler<DeleteFriendsInviteCommand, ICommandResult<FriendsInviteResult>>
{
    private readonly IUsersRepository _usersRepository = usersRepository;

    public async Task<ICommandResult<FriendsInviteResult>> Handle(DeleteFriendsInviteCommand request, CancellationToken cancellationToken)
    {
        User? sender = await _usersRepository.GetUserByIdAsync(new(request.SenderId));
        if (sender is null)
        {
            return new NotFound<FriendsInviteResult>("No user with id {request.SenderId} found.");
        }
        FriendsInvite? friendsInvite = sender.FriendsInvites.SingleOrDefault(fi => fi.Id.Value == request.InviteId);
        if (friendsInvite is null)
        {
            return new NotFound<FriendsInviteResult>($"No friends invite found with id {request.InviteId}");
        }
        _usersRepository.Update(sender);
        await _usersRepository.SaveChangesAsync();
        var result = new FriendsInviteResult(
            friendsInvite.Id,
            friendsInvite.SenderId,
            friendsInvite.AddressieId,
            friendsInvite.Message
        );
        return new SuccessResult<FriendsInviteResult>(result);
    }
}
