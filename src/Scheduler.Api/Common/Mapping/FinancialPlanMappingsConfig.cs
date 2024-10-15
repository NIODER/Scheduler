using Mapster;
using Scheduler.Application.Finances.Common;
using Scheduler.Contracts.Finances;
using Scheduler.Domain.FinancialPlanAggregate;
using Scheduler.Domain.FinancialPlanAggregate.Entities;

namespace Scheduler.Api.Common.Mapping;

public class FinancialPlanMappingsConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        RegisterFinancialPlanResultToFinancialPlanResponseMapping(config);
        RegisterChargeResultToChargeResponseMapping(config);
        RegisterUserFinancialPlansListToFinancialPlansListResponseMapping(config);
        RegisterChargeToChargeResultMapping(config);
        RegisterFinancialPlanToFinancialPlanResultMapping(config);
        RegisterFilledFinancialPlanResultToResponseMapping(config);
        RegisterCalculatedChargeResultToResponseMapping(config);
    }

    private static void RegisterFinancialPlanResultToFinancialPlanResponseMapping(TypeAdapterConfig config)
    {
        config.NewConfig<FinancialPlanResult, FinancialPlanResponse>()
            .Map(dest => dest.FinancialId, src => src.FinancialId)
            .Map(dest => dest.Charges, src => src.Charges);
    }

    private static void RegisterChargeResultToChargeResponseMapping(TypeAdapterConfig config)
    {
        config.NewConfig<ChargeResult, ChargeResponse>()
            .Map(dest => dest.Id, src => src.Charge.Id)
            .Map(dest => dest.ChargeName, src => src.Charge.ChargeName)
            .Map(dest => dest.Description, src => src.Charge.Description)
            .Map(dest => dest.MinimalCost, src => src.Charge.MinimalCost)
            .Map(dest => dest.MaximalCost, src => src.Charge.MaximalCost)
            .Map(dest => dest.Priority, src => src.Charge.Priority)
            .Map(dest => dest.RepeatType, src => src.Charge.RepeatType)
            .Map(dest => dest.ScheduledDate, src => src.Charge.ScheduledDate)
            .Map(dest => dest.ExpirationDate, src => src.Charge.ExpirationDate)
            .Map(dest => dest.Created, src => src.Charge.CreatedDate)
            .IgnoreNonMapped(true);
    }

    private static void RegisterUserFinancialPlansListToFinancialPlansListResponseMapping(TypeAdapterConfig config)
    {
        config.NewConfig<UserFinancialPlansListResult, FinancialPlansListResponse>()
            .Map(dest => dest.Count, src => src.Plans.Count)
            .Map(dest => dest.Plans, src => src.Plans)
            .IgnoreNonMapped(true);
    }

    private static void RegisterChargeToChargeResultMapping(TypeAdapterConfig config)
    {
        config.NewConfig<Charge, ChargeResult>()
            .Map(dest => dest.Charge, src => src);
    }

    private static void RegisterFinancialPlanToFinancialPlanResultMapping(TypeAdapterConfig config)
    {
        config.NewConfig<FinancialPlan, FinancialPlanResult>()
            .Map(dest => dest.FinancialId, src => src.Id.Value)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Charges, src => src.Charges);
    }

    private static void RegisterFilledFinancialPlanResultToResponseMapping(TypeAdapterConfig config)
    {
        config.NewConfig<FilledFinancialPlanResult, FilledFinancialPlanResponse>()
            .Map(dest => dest.FinancialId, src => src.FinancialPlan.Id.Value)
            .Map(dest => dest.Title, src => src.FinancialPlan.Title)
            .Map(dest => dest.LimitDateOptimistic, src => src.LimitDateOptimistic)
            .Map(dest => dest.LimitDatePessimistic, src => src.LimitDatePessimistic)
            .Map(dest => dest.Charges, src => src.Charges);
    }

    private static void RegisterCalculatedChargeResultToResponseMapping(TypeAdapterConfig config)
    {
        config.NewConfig<CalculatedChargeResult, CalculatedChargeResponse>()
            .Map(dest => dest.Id, src => src.Charge.Id.Value)
            .Map(dest => dest.ChargeName, src => src.Charge.ChargeName)
            .Map(dest => dest.Description, src => src.Charge.Description)
            .Map(dest => dest.MinimalCost, src => src.Charge.MinimalCost)
            .Map(dest => dest.MaximalCost, src => src.Charge.MaximalCost)
            .Map(dest => dest.Priority, src => src.Charge.Priority)
            .Map(dest => dest.ExpirationDates, src => src.ExpirationDates)
            .Map(dest => dest.Status, src => src.Status.ToString());
    }
}
