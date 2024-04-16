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
        ConfigureCharges(builder);
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
            cBuilder.HasKey(c => c.Id);
            cBuilder.Property(c => c.Id)
                .HasConversion(
                    id => id.Value,
                    value => new(value)
                )
                .ValueGeneratedNever();

            cBuilder.Property(c => c.ChargeName)
                .HasMaxLength(120)
                .IsRequired();
            
            cBuilder.Property(c => c.Description)
                .HasMaxLength(1000);
            
            cBuilder.Property(c => c.MinimalCost)
                .IsRequired();
            
            cBuilder.Property(c => c.MaximalCost);

            cBuilder.Property(c => c.Priority)
                .IsRequired();
            
            cBuilder.Property(c => c.Repeat)
                .IsRequired();
            
            cBuilder.Property(c => c.ExpireDays)
                .IsRequired();

            cBuilder.Property(c => c.Created)
               .IsRequired();

            cBuilder.WithOwner().HasForeignKey(nameof(FinancialPlanId));
        });

        builder.Metadata.FindNavigation(nameof(FinancialPlan.Charges))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}