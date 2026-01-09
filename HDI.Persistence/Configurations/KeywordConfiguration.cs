using HDI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HDI.Persistence.Configurations;
public class KeywordConfiguration : BaseEntityConfiguration<Keyword, int>
{
    public override void Configure(EntityTypeBuilder<Keyword> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Word).IsRequired().HasMaxLength(100);
        builder.Property(x => x.RiskWeight).HasPrecision(18, 2);

        builder.HasOne(x => x.Agreement)
               .WithMany(a => a.Keywords)
               .HasForeignKey(x => x.AgreementId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
