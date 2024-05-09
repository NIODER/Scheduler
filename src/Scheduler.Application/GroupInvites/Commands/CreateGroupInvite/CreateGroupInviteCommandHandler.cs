using MediatR;
using Microsoft.Extensions.Logging;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Interfaces.Services;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.GroupInvites.Common;
using Scheduler.Domain.GroupAggregate;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate;

namespace Scheduler.Application.GroupInvites.Commands.CreateGroupInvite;

public class CreateGroupInviteCommandHandler(
    IGroupsRepository groupsRepository,
    IUsersRepository usersRepository,
    ILogger<CreateGroupInviteCommandHandler> logger,
    IDateTimeProvider dateTimeProvider) : IRequestHandler<CreateGroupInviteCommand, AccessResultWrapper<GroupInviteResult>>
{
    private readonly IGroupsRepository _groupsRepository = groupsRepository;
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly ILogger<CreateGroupInviteCommandHandler> _logger = logger;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

    public async Task<AccessResultWrapper<GroupInviteResult>> Handle(CreateGroupInviteCommand request, CancellationToken cancellationToken)
    {
        Group group = await _groupsRepository.GetGroupByIdAsync(new(request.GroupId))
            ?? throw new NullReferenceException($"No group with id {request.GroupId} found.");
        User user = await _usersRepository.GetUserByIdAsync(new(request.CreatorId))
            ?? throw new NullReferenceException($"No user with id {request.CreatorId} found.");
        GroupUser? groupUser = group.Users.SingleOrDefault(u => u.UserId == user.Id);
        if (groupUser is null)
        {
            _logger.LogDebug("User with id {userId} is not a member of group with id {groupId}.", user.Id.Value, group.Id.Value);
            return AccessResultWrapper<GroupInviteResult>.CreateForbidden();
        }
        if (!groupUser.Permissions.HasFlag(UserGroupPermissions.CreateInvite))
        {
            _logger.LogDebug("User with id {userId} has no permissions to create group invite for group with id {groupId}", user.Id.Value, group.Id.Value);
            return AccessResultWrapper<GroupInviteResult>.CreateForbidden();
        }
        var invite = group.CreateInvite(user.Id, (UserGroupPermissions)request.Permissions, request.Message, _dateTimeProvider.UtcNow, request.Usages);
        _groupsRepository.Update(group);
        await _groupsRepository.SaveChangesAsync();
        var result = new GroupInviteResult(
            group.Id,
            invite.Id,
            user.Id,
            invite.Permissions,
            invite.Message
        );
        return AccessResultWrapper<GroupInviteResult>.Create(result);
    }
}
