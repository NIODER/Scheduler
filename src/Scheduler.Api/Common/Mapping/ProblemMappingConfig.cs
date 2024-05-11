using Mapster;
using Scheduler.Application.Problems.Common;
using Scheduler.Contracts.Problems;
using Scheduler.Domain.ProblemAggregate;

namespace Scheduler.Api.Common.Mapping;

internal class ProblemMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        ConfigureProblem(config);
        ConfigureUserProblem(config);
        ConfigureProblemProblemResult(config);
        ConfigureGroupProblem(config);
    }

    private static void ConfigureProblem(TypeAdapterConfig config)
    {
        config.NewConfig<ProblemResult, ProblemResponse>()
            .Map(dest => dest.TaskId, src => src.TaskId.Value)
            .Map(dest => dest.GroupId, src => src.GroupId == null ? null : (Guid?)src.GroupId.Value)
            .Map(dest => dest.UserId, src => src.UserId == null ? null : (Guid?)src.UserId.Value)
            .Map(dest => dest.CreatorId, src => src.CreatorId.Value)
            .Map(dest => dest.Status, src => src.Status.ToString());
    }

    private static void ConfigureUserProblem(TypeAdapterConfig config)
    {
        config.NewConfig<UserProblemsResult, UserProblemsResponse>()
            .Map(dest => dest.Count, src => src.Tasks.Count)
            .Map(dest => dest.UserId, src => src.UserId.Value);
    }

    private static void ConfigureProblemProblemResult(TypeAdapterConfig config)
    {
        config.NewConfig<Problem, ProblemResult>()
            .Map(dest => dest.TaskId, src => src.Id);
    }

    private static void ConfigureGroupProblem(TypeAdapterConfig config)
    {
        config.NewConfig<GroupProblemsResult, GroupProblemsResponse>()
            .Map(dest => dest.Count, src => src.Tasks.Count)
            .Map(dest => dest.GroupId, src => src.GroupId.Value);
    }
}
