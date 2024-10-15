using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Finances.Common;
using Scheduler.Domain.FinancialPlanAggregate;
using Scheduler.Domain.GroupAggregate;

namespace Scheduler.Application.Finances.Commands.DeleteFinancialPlan;

internal class DeleteFinancialPlanCommandHandler(
    IFinancialPlansRepository financialPlansRepository,
    IGroupsRepository groupsRepository,
    IUsersRepository usersRepository,
    ILogger<DeleteFinancialPlanCommandHandler> logger,
    IMapper mapper) : IRequestHandler<DeleteFinancialPlanCommand, ICommandResult<FinancialPlanResult>>
{
    private readonly IFinancialPlansRepository _financialPlansRepository = financialPlansRepository;
    private readonly IGroupsRepository _groupsRepository = groupsRepository;
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly ILogger<DeleteFinancialPlanCommandHandler> _logger = logger;
    private readonly IMapper _mapper = mapper;

    public async Task<ICommandResult<FinancialPlanResult>> Handle(DeleteFinancialPlanCommand request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetUserByIdAsync(new(request.UserId));
        if (user is null)
        {
            return new NotFound<FinancialPlanResult>($"No user with id {request.UserId} found.");
        }

        var financialPlan = await _financialPlansRepository.GetFinancialPlanByIdAsync(new(request.FinancialPlanId));
        if (financialPlan is null)
        {
            return new NotFound<FinancialPlanResult>($"No financial plan with id {request.FinancialPlanId} found.");
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
            if (groupUser == default || !groupUser.Permissions.HasFlag(UserGroupPermissions.DeleteUnownedFinancialPlan))
            {
                return new AccessViolation<FinancialPlanResult>($"User with id {user.Id.Value} has no permissions to delete financial plans in this group.");
            }

            group.RemoveFinancialPlan(financialPlan.Id);
            _groupsRepository.Update(group);
            await _groupsRepository.SaveChangesAsync();
        }

        _financialPlansRepository.DeleteFinancialPlanById(financialPlan.Id);
        await _financialPlansRepository.SaveChangesAsync();

        return new SuccessResult<FinancialPlanResult>(_mapper.Map<FinancialPlan, FinancialPlanResult>(financialPlan));
    }
}
