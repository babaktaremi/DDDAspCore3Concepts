using System;
using System.Security.Claims;

namespace Application.Services.Identity.PermissionManager
{
    public class DynamicPermissionService : IDynamicPermissionService
    {
        public bool CanAccess(ClaimsPrincipal user, string area, string controller, string action)
        {
            var key = $"{area}:{controller}:{action}";

            var userClaims = user.FindAll(ConstantPolicies.DynamicPermission);

            foreach (var item in userClaims)
            {
                if (item.Value.Equals(key,StringComparison.OrdinalIgnoreCase))
                    return true;
                else
                {
                    continue;
                }
               
            }

            return false;
        }
    }
}