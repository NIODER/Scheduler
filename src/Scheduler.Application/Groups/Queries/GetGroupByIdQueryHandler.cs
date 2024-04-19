using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Groups.Common;
using Scheduler.Domain.GroupAggregate;

namespace Scheduler.Application.Groups.Queries;

public class GetGroupByIdQueryHandler(IGroupsRepository groupsRepository) : IRequestHandler<GetGroupByIdQuery, GroupResult>
{
    public readonly IGroupsRepository _groupsRepository = groupsRepository;

    public Task<GroupResult> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken)
    {
        Group group = _groupsRepository.GetGroupById(new(request.GroupId))
            ?? throw new NullReferenceException($"No group with id {request.GroupId} was found.");
        return Task.FromResult(new GroupResult(group));
    }
}
