using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.UserAggregate;

namespace Infrastructure.Repositories.EFCore.UserRepositories.Contracts
{
   public interface IUserRoleRepository
   {
       void RemoveUserRoles(IEnumerable<UserRole> userRoles);
   }
}
