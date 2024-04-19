using Mapster;
using Scheduler.Application.Groups.Common;
using Scheduler.Contracts.Groups;
using Scheduler.Domain.GroupAggregate.ValueObjects;

namespace Scheduler.Api.Common.Mapping;

public class GroupMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<GroupResult, GroupResponse>()
            .Map(dest => dest.GroupId, src => src.Group.Id.Value)
            .Map(dest => dest.GroupName, src => src.Group.GroupName)
            .Map(dest => dest.Users, src => src.Group.Users);
        
        config.NewConfig<GroupUser, GroupUserResponse>()
            .Map(dest => dest.UserId, src => src.UserId.Value)
            .Map(dest => dest.Permissions, src => (int)src.Permissions);
        
        config.NewConfig<GroupUserResult, GroupUserResponse>()
            .Map(dest => dest.UserId, src => src.UserId.Value)
            .Map(dest => dest.Permissions, src => (int)src.Permissions)
            .IgnoreNonMapped(true);
    }
}
