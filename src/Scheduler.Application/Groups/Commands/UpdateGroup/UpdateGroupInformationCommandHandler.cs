using MediatR;
using Microsoft.Extensions.Logging;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Groups.Common;
using Scheduler.Domain.GroupAggregate;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Application.Groups.Commands.UpdateGroup;

public class UpdateGroupInformationCommandHandler(IGroupsRepository groupsRepository, ILogger<UpdateGroupInformationCommandHandler> logger) 
    : IRequestHandler<UpdateGroupInformationCommand, ICommandResult<GroupResult>>
{
    private readonly IGroupsRepository _groupsRepository = groupsRepository;
    private readonly ILogger<UpdateGroupInformationCommandHandler> _logger = logger;

    public async Task<ICommandResult<GroupResult>> Handle(UpdateGroupInformationCommand request, CancellationToken cancellationToken)
    {
        Group? group = await _groupsRepository.GetGroupByIdAsync(new(request.GroupId));
        if (group is null)
        {
            return new NotFound<GroupResult>($"No group with id {request.GroupId} found.");
        }
        if (!group.UserHasPermissions(new UserId(request.ExecutorId), UserGroupPermissions.ChangeGroupSettings))
        {
            return new AccessViolation<GroupResult>("No permissions.");
        }
        group.GroupName = request.GroupName;
        _groupsRepository.Update(group);
        await _groupsRepository.SaveChangesAsync();
        _logger.LogInformation("Group {groupId} information updated.", request.GroupId);
        return new SuccessResult<GroupResult>(new GroupResult(group));
    }
}
