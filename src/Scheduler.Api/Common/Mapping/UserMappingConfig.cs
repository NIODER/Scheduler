using Mapster;
using Scheduler.Application.Users.Common;
using Scheduler.Contracts.Users;

namespace Scheduler.Api.Common.Mapping;

public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UserSettingsResult, UserSettingsResponse>()
            .Map(src => src.Settings, dest => (int)dest.Settings);
    }
}
