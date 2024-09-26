using Lamba.Domain.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lamba.Infrastructure.Data.Configurations
{
    public class BaseAuditEntityConfiguration<TEntity, TKey> : BaseEntityConfiguration<TEntity, TKey>
        where TEntity : BaseAuditEntity<TKey>
        where TKey : struct
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.UpdatedAt).IsRequired(false);
            builder.Property(x => x.DeletedAt).IsRequired(false);
            builder.HasQueryFilter(x => x.DeletedAt == null);
            builder.HasIndex(x => new
            {
                x.Id,
                x.DeletedAt
            });
        }
    }
}
