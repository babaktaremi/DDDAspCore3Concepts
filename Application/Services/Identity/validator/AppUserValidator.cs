using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.UserAggregate;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.Identity.validator
{
    public class AppUserValidator:UserValidator<User>
    {
        public AppUserValidator(IdentityErrorDescriber errors):base(errors)
        {
            
        }

        public override async Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
        {
            var result=await base.ValidateAsync(manager, user);

            return result;
        }

        
    }
}
