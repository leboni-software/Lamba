using Lamba.Domain.Concrete;
using Lamba.Infrastructure.Data.Configurations.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lamba.Infrastructure.Data.Configurations
{
    public class BaseEntityConfiguration<TEntity, TKey> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity<TKey>
        where TKey : struct
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            if (typeof(TKey).Equals(typeof(int)) || typeof(TKey).Equals(typeof(long)) || typeof(TKey).Equals(typeof(short)))
                builder.Property(x => x.Id).IsRequired().UseIdentityColumn().ValueGeneratedOnAdd();
            else if (typeof(TKey).Equals(typeof(Guid)))
                builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd().HasDefaultValueSql(NpgsqlEntityConfigurationConstant.DefaultRandomUUID);
            builder.Property(x => x.CreatedAt).IsRequired().ValueGeneratedOnAdd().HasDefaultValueSql(NpgsqlEntityConfigurationConstant.DefaultDateTimeUTC);
            builder.Property(x => x.CreatedUserId).IsRequired(false);
        }
    }
}
