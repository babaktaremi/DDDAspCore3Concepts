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
}
