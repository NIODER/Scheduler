using Mapster;
using Scheduler.Application.Users.Common;
using Scheduler.Contracts.Invites.UserInvites;
using Scheduler.Contracts.Users;

namespace Scheduler.Api.Common.Mapping;

public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UserSettingsResult, UserSettingsResponse>()
            .Map(src => src.Settings, dest => (int)dest.Settings);
        AddUserInviteMapping(config);
    }

    public void AddUserInviteMapping(TypeAdapterConfig config)
    {
        config.NewConfig<UserInviteResult, UserInvitesResponse>()
            .Map(src => src.InviteId, dest => dest.InviteId.Value)
            .Map(src => src.AddressieId, dest => dest.AddressieId.Value)
            .Map(src => src.SenderId, dest => dest.SenderId.Value);
    }
}
