using Mapster;
using Scheduler.Application.Groups.Common;
using Scheduler.Contracts.Groups.GroupInvites;

namespace Scheduler.Api.Common.Mapping;

internal class GroupInviteMapingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<GroupInviteResult, GroupInviteResponse>()
            .Map(dest => dest.InviteId, src => src.InviteId.Value)
            .Map(dest => dest.Permissions, src => (long)src.Permissions)
            .Map(dest => dest.SenderId, src => src.SenderId.Value)
            .Map(dest => dest.Message, src => src.Message)
            .Map(dest => dest.GroupId, src => src.GroupId.Value);
    }
}
