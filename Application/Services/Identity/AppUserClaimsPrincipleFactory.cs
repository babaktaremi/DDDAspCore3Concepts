using System.Security.Claims;
using System.Threading.Tasks;
using Application.Services.Identity.Manager;
using Domain.UserAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Application.Services.Identity
{
    public class AppUserClaimsPrincipleFactory:UserClaimsPrincipalFactory<User,Role>
    {
        public AppUserClaimsPrincipleFactory(AppUserManager userManager, AppRoleManager roleManager, IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
        }

       
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var userRoles = await UserManager.GetRolesAsync(user);

            var claimsIdentity = await base.GenerateClaimsAsync(user);
            claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier,user.Id.ToString(),ClaimValueTypes.Integer));
            //claimsIdentity.AddClaim(new Claim(ClaimTypes.Email,user?.Email));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name,user.UserName));
           // claimsIdentity.AddClaim(new Claim(ClaimTypes.MobilePhone,user.PhoneNumber));


            foreach (var roles in userRoles)
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role,roles));
            }

            //claimsIdentity.AddClaim(new Claim(ClaimTypes.Role,RoleManager.GetRoleNameAsync(user.Roles)));
            

            return claimsIdentity;
        }
    }
}
