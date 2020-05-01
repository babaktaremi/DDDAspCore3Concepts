using System;
using System.Collections.Generic;
using Domain.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.UserAggregate
{
   public class User:IdentityUser<int>,IEntity
    {
        public User()
        {
            this.GeneratedCode = Guid.NewGuid().ToString().Substring(0, 8);
        }

        public string GeneratedCode { get; set; }
       
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<UserLogin> Logins { get; set; }
        public ICollection<UserClaim> Claims { get; set; }
        public ICollection<UserToken> Tokens { get; set; }
    }

   public partial class DbConfiguration : IEntityTypeConfiguration<User>
   {
       public void Configure(EntityTypeBuilder<User> builder)
       {

           builder.ToTable("Users").Property(p => p.Id).HasColumnName("UserId");
       }
   }
}
