using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configs.UserConfigs
{
   public class UserLoginConfig:IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            builder.HasOne(u => u.User).WithMany(u => u.Logins).HasForeignKey(u => u.UserId);
            builder.ToTable("UserLogins");
        }
    }
}
