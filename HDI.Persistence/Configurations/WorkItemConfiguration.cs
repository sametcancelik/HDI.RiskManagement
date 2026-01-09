using HDI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HDI.Persistence.Configurations;

public class WorkItemConfiguration : BaseEntityConfiguration<WorkItem, int>
{
    public override void Configure(EntityTypeBuilder<WorkItem> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.CalculatedRiskAmount).HasPrecision(18, 2);

        builder.HasOne(x => x.Agreement)
               .WithMany(a => a.WorkItems)
               .HasForeignKey(x => x.AgreementId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
