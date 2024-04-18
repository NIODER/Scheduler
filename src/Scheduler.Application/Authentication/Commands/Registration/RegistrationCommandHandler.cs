using MediatR;
using Scheduler.Application.Authentication.Common;
using Scheduler.Application.Common.Interfaces.Authentication;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Interfaces.Services;
using Scheduler.Domain.UserAggregate;

namespace Scheduler.Application.Authentication.Commands.Registration;

public class RegistrationCommandHandler(
    IJwtTokenGenerator jwtTokenGenerator, 
    IUsersRepository usersRepository,
    IHashProvider hashProvider) : IRequestHandler<RegisterCommand, AuthenticationResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly IHashProvider _hashProvider = hashProvider;

    public Task<AuthenticationResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (_usersRepository.GetUserByEmail(request.Email) is not null)
        {
            throw new Exception("Email is already taken.");
        }
        User user = User.Create(
            request.Username,
            request.Email,
            request.Description,
            _hashProvider.GetHash(request.Password)
        );
        _usersRepository.Add(user);
        _usersRepository.SaveChanges();
        var token = _jwtTokenGenerator.GenerateJwtToken(user);
        return Task.FromResult(new AuthenticationResult(user, token));
    }
}
