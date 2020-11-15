using System;
using System.Collections.Generic;
using Domain.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.UserAggregate
{
    public class Role:IdentityRole<int>,IEntity
    {
        public Role()
        {
            CreatedDate=DateTime.Now;
        }

        public string DisplayName { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<RoleClaim> Claims { get; set; }
        public ICollection<UserRole> Users { get; set; }


    }
   
}
