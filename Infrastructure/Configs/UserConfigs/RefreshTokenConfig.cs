using Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configs.UserConfigs
{
   public class RefreshTokenConfig:IEntityTypeConfiguration<UserRefreshToken>
    {
        public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
        {
            builder.HasOne(c => c.User).WithMany(c => c.UserRefreshTokens).HasForeignKey(c => c.UserId);
        }
    }
}
