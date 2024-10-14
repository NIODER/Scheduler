using Mapster;
using Scheduler.Application.FriendsInvites.Common;
using Scheduler.Application.Users.Common;
using Scheduler.Contracts.Users;
using Scheduler.Contracts.Users.UserInvites;

namespace Scheduler.Api.Common.Mapping;

internal class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UserSettingsResult, UserSettingsResponse>()
            .Map(dest => dest.UserId, src => src.UserId.Value)
            .Map(dest => dest.Settings, src => (int)src.Settings);
        AddUserInviteMapping(config);
    }

    public void AddUserInviteMapping(TypeAdapterConfig config)
    {
        config.NewConfig<FriendsInviteResult, UserInvitesResponse>()
            .Map(dest => dest.InviteId, src => src.InviteId.Value)
            .Map(dest => dest.AddressieId, src => src.AddressieId.Value)
            .Map(dest => dest.SenderId, src => src.SenderId.Value);
    }
}
