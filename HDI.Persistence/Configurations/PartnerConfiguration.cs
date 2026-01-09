using HDI.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HDI.Persistence.Configurations;
public class PartnerConfiguration : BaseEntityConfiguration<Partner, int>
{
    public override void Configure(EntityTypeBuilder<Partner> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
        builder.Property(x => x.ApiKey).IsRequired().HasMaxLength(100);
    }
}
