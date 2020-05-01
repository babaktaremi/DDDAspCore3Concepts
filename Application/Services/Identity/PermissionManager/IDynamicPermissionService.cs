using System.Security.Claims;

namespace Application.Services.Identity.PermissionManager
{
    public interface IDynamicPermissionService
    {
        bool CanAccess(ClaimsPrincipal user, string area, string controller, string action);
    }
}   