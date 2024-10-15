using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Finances.Common;
using Scheduler.Domain.GroupAggregate;

namespace Scheduler.Application.Finances.Commands.UpdateFinancialPlan;

internal class UpdateFinancialPlanCommandHandler(
    IUsersRepository usersRepository,
    IFinancialPlansRepository financialPlansRepository,
    IGroupsRepository groupsRepository,
    ILogger<UpdateFinancialPlanCommandHandler> logger,
    IMapper mapper) : IRequestHandler<UpdateFinancialPlanCommand, ICommandResult<FinancialPlanResult>>
{
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly IFinancialPlansRepository _financialPlansRepository = financialPlansRepository;
    private readonly IGroupsRepository _groupsRepository = groupsRepository;
    private readonly ILogger<UpdateFinancialPlanCommandHandler> _logger = logger;
    private readonly IMapper _mapper = mapper;

    public async Task<ICommandResult<FinancialPlanResult>> Handle(UpdateFinancialPlanCommand request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetUserByIdAsync(new(request.UserId));
        if (user is null)
        {
            return new NotFound<FinancialPlanResult>($"No user with id {request.UserId} was found.");
        }

        var financialPlan = await _financialPlansRepository.GetFinancialPlanByIdAsync(new(request.FinancialId));
        if (financialPlan is null)
        {
            return new NotFound<FinancialPlanResult>($"No financial plan with id {request.FinancialId} was found.");
        }

        if (!financialPlan.IsPrivate)
        {
            var group = await _groupsRepository.GetGroupByIdAsync(financialPlan.GroupId!);
            if (group is null)
            {
                _logger.LogError("Financial plan {financialPlanId} refers to unexisting group {groupId}", financialPlan.Id, financialPlan.GroupId);
                return new InternalError<FinancialPlanResult>(message: $"Financial plan with id {financialPlan.Id.Value} refers to unexisting group.");
            }

            var groupUser = group.Users.SingleOrDefault(u => u.UserId == user.Id);
            if (groupUser == default || !groupUser.Permissions.HasFlag(UserGroupPermissions.ChangeUnownedFinancialPlan))
            {
                return new AccessViolation<FinancialPlanResult>("User has no permissions to moderate financial plans in this group.");
            }
        }

        if (financialPlan.Title != request.Title)
        {
            financialPlan.Title = request.Title;
            _financialPlansRepository.Update(financialPlan);
            await _financialPlansRepository.SaveChangesAsync();
        }

        return new SuccessResult<FinancialPlanResult>(_mapper.Map<FinancialPlanResult>(financialPlan));
    }
}
