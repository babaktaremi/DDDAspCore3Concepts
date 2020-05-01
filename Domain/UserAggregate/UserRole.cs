using System;
using Domain.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.UserAggregate
{
    public class UserRole : IdentityUserRole<int>,IEntity
    {
        public User User { get; set; }
        public Role Role { get; set; }
        public DateTime CreatedUserRoleDate { get; set; }

    }
    public partial class DbConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {

            builder.HasOne(u => u.User).WithMany(u => u.UserRoles).HasForeignKey(u => u.UserId);
            builder.HasOne(u => u.Role).WithMany(u => u.Users).HasForeignKey(u => u.RoleId);
            builder.ToTable("UserRoles");
        }
    }
}
