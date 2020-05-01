using Domain.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.UserAggregate
{
    public class UserClaim:IdentityUserClaim<int>,IEntity
    {
        public User User { get; set; }
    }
    public partial class DbConfiguration : IEntityTypeConfiguration<UserClaim>
    {
        public void Configure(EntityTypeBuilder<UserClaim> builder)
        {

            builder.HasOne(u => u.User).WithMany(u => u.Claims).HasForeignKey(u => u.UserId);
            builder.ToTable("UserClaims");
        }
    }
}
