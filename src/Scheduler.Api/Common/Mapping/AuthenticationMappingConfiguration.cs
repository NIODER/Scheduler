using Mapster;
using Scheduler.Application.Authentication.Common;
using Scheduler.Contracts.Authentication;

namespace Scheduler.Api.Common.Mapping;

internal class AuthenticationMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Id, src => src.User.Id.Value)
            .Map(dest => dest, src => src.User);
    }
}
