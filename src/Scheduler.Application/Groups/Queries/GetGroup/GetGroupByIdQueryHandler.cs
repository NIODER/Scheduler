using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Groups.Common;
using Scheduler.Domain.GroupAggregate;

namespace Scheduler.Application.Groups.Queries.GetGroup;

public class GetGroupByIdQueryHandler(IGroupsRepository groupsRepository) 
    : IRequestHandler<GetGroupByIdQuery, ICommandResult<GroupResult>>
{
    public readonly IGroupsRepository _groupsRepository = groupsRepository;

    public async Task<ICommandResult<GroupResult>> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken)
    {
        Group? group = await _groupsRepository.GetGroupByIdAsync(new(request.GroupId));
        if (group is null)
        {
            return new NotFound<GroupResult>($"No group with id {request.GroupId} was found.");
        }
        return new SuccessResult<GroupResult>(new GroupResult(group));
    }
}
