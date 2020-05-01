using System;
using Domain.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.UserAggregate
{
    public class RoleClaim:IdentityRoleClaim<int>,IEntity
    {
        public RoleClaim()
        {
            CreatedClaim=DateTime.Now;
        }

        public DateTime CreatedClaim { get; set; }
        public Role Role { get; set; }

    }
    public class RoleClaimsConfiguration : IEntityTypeConfiguration<RoleClaim>
    {
        public void Configure(EntityTypeBuilder<RoleClaim> builder)
        {
            builder.ToTable("RoleClaims");
            builder.HasOne(u => u.Role).WithMany(u => u.Claims).HasForeignKey(u => u.RoleId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
