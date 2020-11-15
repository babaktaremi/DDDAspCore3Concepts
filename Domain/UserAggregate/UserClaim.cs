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
  
}
