using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scheduler.Domain.FinancialPlanAggregate;
using Scheduler.Domain.FinancialPlanAggregate.ValueObjects;

namespace Scheduler.Infrastructure.Persistance.Configurations;

public sealed class FinancialPlanConfiguration : IEntityTypeConfiguration<FinancialPlan>
{
    public void Configure(EntityTypeBuilder<FinancialPlan> builder)
    {
        ConfigureProperties(builder);
    }

    private static void ConfigureProperties(EntityTypeBuilder<FinancialPlan> builder)
    {
        builder.HasKey(f => f.Id);
        builder.Property(f => f.Id)
            .HasConversion(
                id => id.Value,
                value => new(value)
            )
            .ValueGeneratedNever();
        
        builder.Property(f => f.Title)
            .HasMaxLength(120)
            .IsRequired();
    }

    private static void ConfigureCharges(EntityTypeBuilder<FinancialPlan> builder)
    {
        builder.OwnsMany(f => f.Charges, cBuilder =>
        {
            cBuilder.WithOwner().HasForeignKey(nameof(FinancialPlanId));
            cBuilder.HasKey(f => f.Id);

            cBuilder.Property(f => f.ChargeName)
                .HasMaxLength(120)
                .IsRequired();
            
            cBuilder.Property(f => f.Description)
                .HasMaxLength(1000);
            
            cBuilder.Property(f => f.MinimalCost)
                .IsRequired();
            
            cBuilder.Property(f => f.MaximalCost);

            cBuilder.Property(f => f.Priority)
                .IsRequired();
            
            cBuilder.Property(f => f.Repeat)
                .IsRequired();
            
            cBuilder.Property(f => f.ExpireDays)
                .IsRequired();

            cBuilder.Property(f => f.Created)
                .IsRequired();
        });

        builder.Metadata.FindNavigation(nameof(FinancialPlan.Charges))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}