using Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configs.UserConfigs
{
    internal class UserLoginConfig:IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            builder.HasOne(u => u.User).WithMany(u => u.Logins).HasForeignKey(u => u.UserId);
            builder.ToTable("UserLogins");
        }
    }
}
