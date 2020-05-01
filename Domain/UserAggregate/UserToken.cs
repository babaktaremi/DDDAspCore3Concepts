using System;
using Domain.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.UserAggregate
{
    public class UserToken:IdentityUserToken<int>,IEntity
    {
        public UserToken()
        {
            GeneratedTime=DateTime.Now;
        }

        public User User { get; set; }
        public DateTime GeneratedTime { get; set; }

    }
    public partial class DbConfiguration : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {

            builder.HasOne(u => u.User).WithMany(u => u.Tokens).HasForeignKey(u => u.UserId);
            builder.ToTable("UserTokens");
        }
    }
}
