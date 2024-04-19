using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Groups.Common;
using Scheduler.Domain.GroupAggregate;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Application.Groups.Commands.DeleteGroup;

public class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand, AccessResultWrapper<GroupResult>>
{
    private readonly IGroupsRepository _groupsRepository;

    public DeleteGroupCommandHandler(IGroupsRepository groupsRepository)
    {
        _groupsRepository = groupsRepository;
    }

    public Task<AccessResultWrapper<GroupResult>> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {
        Group group = _groupsRepository.GetGroupById(new(request.GroupId))
            ?? throw new NullReferenceException($"No group with id {request.GroupId} found.");
        if (!group.UserHasPermissions(new UserId(request.ExecutorId), UserGroupPermissions.IsGroupOwner))
        {
            return Task.FromResult(AccessResultWrapper<GroupResult>.CreateForbidden(clientMessage: "Delete group can only group owner."));
        }
        _groupsRepository.DeleteGroupById(group.Id);
        _groupsRepository.SaveChanges();
        return Task.FromResult(AccessResultWrapper<GroupResult>.Create(new(group)));
    }
}
