using System.Threading.Tasks;
using Domain.UserAggregate;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.Identity.validator
{
    public class AppRoleValidator:RoleValidator<Role>
    {
        public AppRoleValidator(IdentityErrorDescriber errors):base(errors)
        {
            
        }

        public override Task<IdentityResult> ValidateAsync(RoleManager<Role> manager, Role role)
        {
            return base.ValidateAsync(manager, role);
        }
    }
}
