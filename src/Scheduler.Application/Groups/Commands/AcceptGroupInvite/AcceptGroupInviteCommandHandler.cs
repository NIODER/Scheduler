using MediatR;
using Microsoft.Extensions.Logging;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Interfaces.Services;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Groups.Common;
using Scheduler.Domain.GroupAggregate;
using Scheduler.Domain.GroupAggregate.Entities;

namespace Scheduler.Application.Groups.Commands.AcceptGroupInvite;

public class AcceptGroupInviteCommandHandler(IGroupsRepository groupsRepository, IDateTimeProvider dateTimeProvider, ILogger<AcceptGroupInviteCommandHandler> logger)
    : IRequestHandler<AcceptGroupInviteCommand, ICommandResult<GroupInviteResult>>
{
    private readonly IGroupsRepository _groupsRepository = groupsRepository;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;
    private readonly ILogger<AcceptGroupInviteCommandHandler> _logger = logger;

    public async Task<ICommandResult<GroupInviteResult>> Handle(AcceptGroupInviteCommand request, CancellationToken cancellationToken)
    {
        Group? group = await _groupsRepository.GetGroupByIdAsync(new(request.GroupId));
        if (group is null)
        {
            return new NotFound<GroupInviteResult>($"No group with id {request.GroupId} found.");
        }
        if (group.Users.Any(u => u.UserId.Value == request.UserId))
        {
            return new ExpectedError<GroupInviteResult>($"User with id {request.UserId} already in group.");
        }
        GroupInvite? invite = group.Invites.SingleOrDefault(i => i.Id.Value == request.InviteId);
        if (invite is null)
        {
            return new NotFound<GroupInviteResult>($"No invite with id {request.InviteId} found.");
        }
        group.AcceptUserInGroup(new(request.UserId), invite.Id, _dateTimeProvider.UtcNow);
        _groupsRepository.Update(group);
        await _groupsRepository.SaveChangesAsync();
        _logger.LogInformation("User {userId} added to group {groupId} users.", request.UserId, request.GroupId);
        var result = new GroupInviteResult(
            group.Id,
            invite.Id,
            new(request.UserId),
            invite.Permissions,
            invite.Message);
        return new SuccessResult<GroupInviteResult>(result);
    }
}
