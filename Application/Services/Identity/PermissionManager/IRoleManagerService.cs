using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Services.Identity.Dtos;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.Identity.PermissionManager
{
   public interface IRoleManagerService
   {
       Task<List<GetRolesDto>> GetRoles();
       Task<IdentityResult> CreateRole(CreateRoleDto model);
       Task<bool> DeleteRole(int roleId);
       Task<List<ActionDescriptionDto>> GetPermissionActions();
       Task<RolePermissionDto> GetRolePermissions(int roleId);
       Task<bool> ChangeRolePermissions(EditRolePermissionsDto model);
   }
}
