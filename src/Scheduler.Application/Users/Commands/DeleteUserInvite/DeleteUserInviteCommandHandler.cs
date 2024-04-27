using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Users.Common;
using Scheduler.Domain.UserAggregate;

namespace Scheduler.Application.Users.Commands.DeleteUserInvite;

public class DeleteUserInviteCommandHandler(IUsersRepository usersRepository)
        : IRequestHandler<DeleteUserInviteCommand, UserInviteResult>
{
    private readonly IUsersRepository _usersRepository = usersRepository;

    public Task<UserInviteResult> Handle(DeleteUserInviteCommand request, CancellationToken cancellationToken)
    {
        User sender = _usersRepository.GetUserById(new(request.SenderId))
            ?? throw new NullReferenceException($"No user with id {request.SenderId} found.");
        var invite = sender.DeleteFriendsInvite(new(request.InviteId));
        _usersRepository.Update(sender);
        _usersRepository.SaveChanges();
        var result = new UserInviteResult(invite.Id, invite.SenderId, invite.AddressieId, invite.Message);
        return Task.FromResult(result); 
    }
}
