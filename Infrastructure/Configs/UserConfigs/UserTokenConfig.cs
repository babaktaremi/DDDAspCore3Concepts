using Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configs.UserConfigs
{
    internal class UserTokenConfig:IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {

            builder.HasOne(u => u.User).WithMany(u => u.Tokens).HasForeignKey(u => u.UserId);
            builder.ToTable("UserTokens");
        }
    }
}
