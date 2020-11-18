using System;
using System.Collections.Generic;
using Domain.Common;
using Domain.OrderAggregate;
using Domain.OrderAggregate.Specs;
using Domain.UserAggregate.Specs;
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
        public ICollection<UserRefreshToken> UserRefreshTokens { get; set; }


        #region Navigation Properties

        public ICollection<Order> Orders { get; set; }

        #endregion


        #region Methods

        public bool CanRegisterOrder()
        {
            var spec = new CanRegisterNewOrderSpec();

            return spec.IsSatisfiedBy(this);
        }

        #endregion
    }

}
