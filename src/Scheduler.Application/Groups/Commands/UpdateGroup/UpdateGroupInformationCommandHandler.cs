using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Groups.Common;
using Scheduler.Domain.GroupAggregate;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Application.Groups.Commands.UpdateGroup;

public class UpdateGroupInformationCommandHandler(IGroupsRepository groupsRepository) : IRequestHandler<UpdateGroupInformationCommand, AccessResultWrapper<GroupResult>>
{
    private readonly IGroupsRepository _groupsRepository = groupsRepository;

    public Task<AccessResultWrapper<GroupResult>> Handle(UpdateGroupInformationCommand request, CancellationToken cancellationToken)
    {
        Group group = _groupsRepository.GetGroupById(new(request.GroupId))
            ?? throw new NullReferenceException($"No group with id {request.GroupId} found.");
        if (!group.UserHasPermissions(new UserId(request.ExecutorId), UserGroupPermissions.ChangeGroupSettings))
        {
            return Task.FromResult(AccessResultWrapper<GroupResult>.CreateForbidden());
        }
        group.GroupName = request.GroupName;
        _groupsRepository.Update(group);
        _groupsRepository.SaveChanges();
        var groupresult = new GroupResult(group);
        return Task.FromResult(AccessResultWrapper<GroupResult>.Create(result: groupresult));
    }
}
