using HDI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HDI.Persistence.Configurations;

public class AgreementConfiguration : BaseEntityConfiguration<Agreement, int>
{
    public override void Configure(EntityTypeBuilder<Agreement> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Title).IsRequired().HasMaxLength(500);
        builder.Property(x => x.RiskLimit).HasPrecision(18, 2); 

        builder.HasOne<Partner>()
               .WithMany(p => p.Agreements)
               .HasForeignKey(x => x.TenantId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
