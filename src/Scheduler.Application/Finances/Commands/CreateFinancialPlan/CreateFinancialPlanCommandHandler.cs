using MapsterMapper;
using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Finances.Common;
using Scheduler.Domain.FinancialPlanAggregate;
using Scheduler.Domain.GroupAggregate;
using Scheduler.Domain.UserAggregate;

namespace Scheduler.Application.Finances.Commands.CreateFinancialPlan;

internal class CreateFinancialPlanCommandHandler(
    IFinancialPlansRepository financialPlansRepository,
    IUsersRepository usersRepository,
    IGroupsRepository groupsRepository,
    IMapper mapper) : IRequestHandler<CreateFinancialPlanCommand, ICommandResult<FinancialPlanResult>>
{
    private readonly IFinancialPlansRepository _financialPlansRepository = financialPlansRepository;
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly IGroupsRepository _groupsRepository = groupsRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ICommandResult<FinancialPlanResult>> Handle(CreateFinancialPlanCommand request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetUserByIdAsync(new(request.UserId));
        if (user is null)
        {
            return new NotFound<FinancialPlanResult>($"No user with id {request.UserId} was found in database.");
        }

        if (request.GroupId is not null)
        {
            return await CreateGroupFinancialPlan(request, user, cancellationToken);
        }
        else
        {
            return CreateUserFinancialPlan(request, user, cancellationToken);
        }
    }

    private SuccessResult<FinancialPlanResult> CreateUserFinancialPlan(CreateFinancialPlanCommand request, User? user, CancellationToken cancellationToken)
    {
        var financialPlan = FinancialPlan.CreatePrivate(request.Title, user!.Id, []);
        user.AddFinancialPlan(financialPlan.Id);

        _financialPlansRepository.Add(financialPlan);
        _usersRepository.Update(user);

        Task.WaitAll([_financialPlansRepository.SaveChangesAsync(), _usersRepository.SaveChangesAsync()], cancellationToken);

        return new SuccessResult<FinancialPlanResult>(_mapper.Map<FinancialPlanResult>(financialPlan));
    }

    private async Task<ICommandResult<FinancialPlanResult>> CreateGroupFinancialPlan(CreateFinancialPlanCommand request, User? user, CancellationToken cancellationToken)
    {
        var group = await _groupsRepository.GetGroupByIdAsync(new(request.GroupId!.Value));
        if (group is null)
        {
            return new NotFound<FinancialPlanResult>($"No group with id {request.GroupId.Value} was found in database.");
        }

        var groupUser = group.Users.SingleOrDefault(u => u.UserId == user!.Id);
        if (groupUser == default || !groupUser.Permissions.HasFlag(UserGroupPermissions.CreateFinancialPlan))
        {
            return new AccessViolation<FinancialPlanResult>($"User has no permissions to create financial plans in this group.");
        }

        var financialPlan = FinancialPlan.CreateGroup(request.Title, user!.Id, group.Id, []);

        _financialPlansRepository.Add(financialPlan);
        _groupsRepository.Update(group);

        Task.WaitAll([_financialPlansRepository.SaveChangesAsync(), _groupsRepository.SaveChangesAsync()], cancellationToken);

        return new SuccessResult<FinancialPlanResult>(_mapper.Map<FinancialPlanResult>(financialPlan));
    }
}
