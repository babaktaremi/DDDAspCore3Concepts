using Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configs.UserConfigs
{
    internal class RoleClaimConfig:IEntityTypeConfiguration<RoleClaim>
    {
        public void Configure(EntityTypeBuilder<RoleClaim> builder)
        {
            builder.ToTable("RoleClaims");
           
            builder.HasOne(u => u.Role).WithMany(u => u.Claims).HasForeignKey(u => u.RoleId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
