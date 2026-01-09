using HDI.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HDI.Persistence.Configurations;

public abstract class BaseEntityConfiguration<TEntity, TId> : IEntityTypeConfiguration<TEntity> 
    where TEntity : BaseEntity<TId>
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(x => x.Id);
        
        if (typeof(IAuditBaseEntity).IsAssignableFrom(typeof(TEntity)))
        {
            builder.Property<string>("CreatedBy").HasMaxLength(100);
            builder.Property<string>("ModifiedBy").HasMaxLength(100);
            builder.Property<string>("DeletedBy").HasMaxLength(100);
        }
    }
}
