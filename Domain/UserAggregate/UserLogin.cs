using System;
using Domain.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.UserAggregate
{
    public class UserLogin:IdentityUserLogin<int>,IEntity
    {
        public UserLogin()
        {
            LoggedOn=DateTime.Now;
        }

        public User User { get; set; }
        public DateTime LoggedOn { get; set; }
    }

    public partial class DbConfiguration:IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            builder.HasOne(u => u.User).WithMany(u => u.Logins).HasForeignKey(u => u.UserId);
            builder.ToTable("UserLogins");
        }
    }
}
