using MediatR;
using Microsoft.Extensions.Logging;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Users.Common;
using Scheduler.Domain.UserAggregate;

namespace Scheduler.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler(IUsersRepository userRepository, ILogger<UpdateUserCommandHandler> logger) 
    : IRequestHandler<UpdateUserCommand, ICommandResult<UserResult>>
{
    private readonly IUsersRepository _userRepository = userRepository;
    private readonly ILogger<UpdateUserCommandHandler> _logger = logger;

    public async Task<ICommandResult<UserResult>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetUserByIdAsync(new(request.UserId));
        if (user is null)
        {
            return new NotFound<UserResult>($"No user with id {request.UserId} found.");
        }
        user.Name = request.Name ?? user.Name;
        user.Description = request.Description ?? user.Description;
        _userRepository.Update(user);
        await _userRepository.SaveChangesAsync();
        _logger.LogInformation("User {userId} updated.", user.Id.Value);
        var result = new UserResult(
            user.Id.Value,
            user.Name,
            user.Email,
            user.Description
        );
        return new SuccessResult<UserResult>(result);
    }
}
