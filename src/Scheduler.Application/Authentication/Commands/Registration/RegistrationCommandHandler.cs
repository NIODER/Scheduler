using MediatR;
using Scheduler.Application.Authentication.Common;
using Scheduler.Application.Common.Interfaces.Authentication;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Interfaces.Services;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Domain.UserAggregate;

namespace Scheduler.Application.Authentication.Commands.Registration;

public class RegistrationCommandHandler(
    IJwtTokenGenerator jwtTokenGenerator,
    IUsersRepository usersRepository,
    IHashProvider hashProvider) : IRequestHandler<RegisterCommand, ICommandResult<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly IHashProvider _hashProvider = hashProvider;

    public async Task<ICommandResult<AuthenticationResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        User? user = await _usersRepository.GetUserByEmailAsync(request.Email);
        if (user is not null)
        {
            return new InvalidData<AuthenticationResult>("Email is already taken.");
        }
        user = User.Create(
            request.Username,
            request.Email,
            request.Description,
            _hashProvider.GetHash(request.Password)
        );
        _usersRepository.Add(user);
        _usersRepository.SaveChanges();
        var token = _jwtTokenGenerator.GenerateJwtToken(user);
        var result = new AuthenticationResult(user, token);
        return new SuccessResult<AuthenticationResult>(result);
    }
}
