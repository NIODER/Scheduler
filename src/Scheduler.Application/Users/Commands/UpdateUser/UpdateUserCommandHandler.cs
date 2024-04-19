using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Users.Common;
using Scheduler.Domain.UserAggregate;

namespace Scheduler.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler(IUsersRepository userRepository) : IRequestHandler<UpdateUserCommand, UserResult>
{
    private readonly IUsersRepository _userRepository = userRepository;

    public Task<UserResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        User user = _userRepository.GetUserById(new(request.UserId))
            ?? throw new NullReferenceException($"No user with id {request.UserId} found.");
        user.Name = request.Name ?? user.Name;
        user.Description = request.Description ?? user.Description;
        var userResult = new UserResult(
            user.Id.Value,
            user.Name,
            user.Email,
            user.Description
        );
        return Task.FromResult(userResult);
    }
}
