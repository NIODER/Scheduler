using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Interfaces.Services;
using Scheduler.Application.GroupInvites.Common;
using Scheduler.Domain.GroupAggregate;
using Scheduler.Domain.GroupAggregate.Entities;

namespace Scheduler.Application.GroupInvites.Commands.AcceptGroupInvite;

public class AcceptGroupInviteCommandHandler(IGroupsRepository groupsRepository, IDateTimeProvider dateTimeProvider)
    : IRequestHandler<AcceptGroupInviteCommand, GroupInviteResult>
{
    private readonly IGroupsRepository _groupsRepository = groupsRepository;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

    public async Task<GroupInviteResult> Handle(AcceptGroupInviteCommand request, CancellationToken cancellationToken)
    {
        Group group = await _groupsRepository.GetGroupByIdAsync(new(request.GroupId))
            ?? throw new NullReferenceException($"No group with id {request.GroupId} found.");
        if (group.Users.Any(u => u.UserId.Value == request.UserId))
        {
            throw new Exception("User already in group.");
        }
        GroupInvite invite = group.Invites.SingleOrDefault(i => i.Id.Value == request.InviteId)
            ?? throw new NullReferenceException($"No invite with id {request.InviteId} found.");
        group.AcceptUserInGroup(new(request.UserId), invite.Id, _dateTimeProvider.UtcNow);
        var result = new GroupInviteResult(
            group.Id,
            invite.Id,
            new(request.UserId), // mb should check if user exists
            invite.Permissions,
            invite.Message);
        return result;
    }
}
