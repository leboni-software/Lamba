using Lamba.Identity.Domain.Entities;
using Lamba.Infrastructure.Data.Configurations;
using Lamba.Infrastructure.Data.Configurations.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lamba.Identity.Infrastructure.Data.Configurations
{
    public class UserRoleConfiguration : BaseAuditEntityConfiguration<UserRole, Guid>
    {
        public override void Configure(EntityTypeBuilder<UserRole> builder)
        {
            base.Configure(builder);
            builder.HasIndex(x => new
            {
                x.UserId,
                x.RoleId
            }).IsUnique()
            .HasFilter(NpgsqlEntityConfigurationConstant.DeletedFilter);

            builder.HasOne(x => x.User)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.UserId)
                .IsRequired();
            builder.HasOne(x => x.Role)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.RoleId)
                .IsRequired();
        }
    }
}
