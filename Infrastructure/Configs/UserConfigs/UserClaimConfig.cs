using Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configs.UserConfigs
{
    internal class UserClaimConfig:IEntityTypeConfiguration<UserClaim>
    {
        public void Configure(EntityTypeBuilder<UserClaim> builder)
        {

            builder.HasOne(u => u.User).WithMany(u => u.Claims).HasForeignKey(u => u.UserId);
            builder.ToTable("UserClaims");
        }
    }
}
