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
            .Map(src => src.UserId, dest => dest.UserId.Value)
            .Map(src => src.Settings, dest => (int)dest.Settings);
        AddUserInviteMapping(config);
    }

    public void AddUserInviteMapping(TypeAdapterConfig config)
    {
        config.NewConfig<FriendsInviteResult, UserInvitesResponse>()
            .Map(src => src.InviteId, dest => dest.InviteId.Value)
            .Map(src => src.AddressieId, dest => dest.AddressieId.Value)
            .Map(src => src.SenderId, dest => dest.SenderId.Value);
    }
}
