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
   public class UserClaimConfig:IEntityTypeConfiguration<UserClaim>
    {
        public void Configure(EntityTypeBuilder<UserClaim> builder)
        {

            builder.HasOne(u => u.User).WithMany(u => u.Claims).HasForeignKey(u => u.UserId);
            builder.ToTable("UserClaims");
        }
    }
}
