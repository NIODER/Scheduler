using MediatR;
using Microsoft.Extensions.Logging;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Groups.Common;
using Scheduler.Domain.GroupAggregate;
using Scheduler.Domain.UserAggregate;

namespace Scheduler.Application.Groups.Commands.CreateGroup;

public class CreateGroupCommandHandler(
    IGroupsRepository groupsRepository,
    IUsersRepository usersRepository,
    ILogger<CreateGroupCommandHandler> logger) 
    : IRequestHandler<CreateGroupCommand, ICommandResult<GroupResult>>
{
    private readonly IGroupsRepository _groupsRepository = groupsRepository;
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly ILogger<CreateGroupCommandHandler> _logger = logger;

    public async Task<ICommandResult<GroupResult>> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        User? user = await _usersRepository.GetUserByIdAsync(new(request.ExecutorId));
        if (user is null)
        {
            return new NotFound<GroupResult>($"No user with id {request.ExecutorId} found.");
        }
        var group = Group.Create(request.GroupName);
        group.AddUser(user.Id, UserGroupPermissions.All);
        _groupsRepository.Add(group);
        await _groupsRepository.SaveChangesAsync();
        _logger.LogInformation("Group created {groupId}.", group.Id.Value);
        return new SuccessResult<GroupResult>(new GroupResult(group)); 
    }
}
