using MapsterMapper;
using MediatR;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Problems.Common;
using Scheduler.Domain.GroupAggregate;

namespace Scheduler.Application.Problems.Commands.DeleteProblem;

public class DeleteProblemCommandHandler(
    IProblemsRepository problemsRepository,
    IUsersRepository usersRepository,
    IGroupsRepository groupsRepository,
    IMapper mapper) : IRequestHandler<DeleteProblemCommand, ICommandResult<ProblemResult>>
{
    private readonly IProblemsRepository _problemsRepository = problemsRepository;
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly IGroupsRepository _groupsRepository = groupsRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ICommandResult<ProblemResult>> Handle(DeleteProblemCommand request, CancellationToken cancellationToken)
    {
        var executor = await _usersRepository.GetUserByIdAsync(new(request.UserId));
        if (executor is null)
        {
            return new InvalidData<ProblemResult>($"No user with id {executor} was found.");
        }

        var problem = await _problemsRepository.GetProblemByIdAsync(new(request.ProblemId));
        if (problem is null)
        {
            return new InvalidData<ProblemResult>($"No task with id {request.ProblemId} was found.");
        }

        Group? group = null;
        if (problem.GroupId is not null)
        {
            group = await _groupsRepository.GetGroupByIdAsync(problem.GroupId);
        }
        try
        {
            problem.Delete(executor.Id, group);
        }
        catch (InvalidOperationException e)
        {
            return new AccessViolation<ProblemResult>(e.Message, e);
        }
        catch (Exception e)
        {
            return new InternalError<ProblemResult>(message: "Can't delete task due to internal server error", exception: e);
        }

        _problemsRepository.DeleteProblemById(problem.Id);
        await _problemsRepository.SaveChangesAsync();
        return new SuccessResult<ProblemResult>(_mapper.Map<ProblemResult>(problem));
    }
}
