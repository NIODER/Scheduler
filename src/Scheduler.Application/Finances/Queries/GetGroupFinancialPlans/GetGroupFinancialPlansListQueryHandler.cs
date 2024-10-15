using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Finances.Common;

namespace Scheduler.Application.Finances.Queries.GetGroupFinancialPlans;

internal class GetGroupFinancialPlansListQueryHandler(
    IGroupsRepository groupsRepository,
    IFinancialPlansRepository financialPlansRepository,
    IUsersRepository usersRepository,
    ILogger<GetGroupFinancialPlansListQueryHandler> logger,
    IMapper mapper) : IRequestHandler<GetGroupFinancialPlansListQuery, ICommandResult<GroupFinancialPlansListResult>>
{
    private readonly IGroupsRepository _groupsRepository = groupsRepository;
    private readonly IFinancialPlansRepository _financialPlansRepository = financialPlansRepository;
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly ILogger<GetGroupFinancialPlansListQueryHandler> _logger = logger;
    private readonly IMapper _mapper = mapper;

    public async Task<ICommandResult<GroupFinancialPlansListResult>> Handle(GetGroupFinancialPlansListQuery request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetUserByIdAsync(new(request.UserId));
        if (user is null)
        {
            return new NotFound<GroupFinancialPlansListResult>("User was not found in database.");
        }

        var group = await _groupsRepository.GetGroupByIdAsync(new(request.GroupId));
        if (group is null)
        {
            return new NotFound<GroupFinancialPlansListResult>($"Group with id {request.GroupId} was not found in database.");
        }

        var groupUser = group.Users.FirstOrDefault(u => u.UserId == user.Id);
        if (!group.Users.Any(u => u.UserId == user.Id))
        {
            return new AccessViolation<GroupFinancialPlansListResult>("User is not a member of group.");
        }

        var financialPlans = await _financialPlansRepository.GetGroupFinancialPlansByGroupIdAsync(group.Id);
        var result = new GroupFinancialPlansListResult(financialPlans.Count, _mapper.Map<List<FinancialPlanResult>>(financialPlans));
        return new SuccessResult<GroupFinancialPlansListResult>(result);
    }
}
