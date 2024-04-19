using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Groups.Common;
using Scheduler.Domain.GroupAggregate;
using Scheduler.Domain.UserAggregate;

namespace Scheduler.Application.Groups.Commands.CreateGroup;

public class CreateGroupCommandHandler(IGroupsRepository groupsRepository, IUsersRepository usersRepository) : IRequestHandler<CreateGroupCommand, GroupResult>
{
    private readonly IGroupsRepository _groupsRepository = groupsRepository;
    private readonly IUsersRepository _usersRepository = usersRepository;

    public Task<GroupResult> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        User user = _usersRepository.GetUserById(new(request.ExecutorId))
            ?? throw new NullReferenceException($"No user with id {request.ExecutorId} found.");
        var group = Group.Create(request.GroupName);
        group.AddUser(user.Id, UserGroupPermissions.All);
        _groupsRepository.Add(group);
        _groupsRepository.SaveChanges();
        return Task.FromResult(new GroupResult(group));
    }
}
