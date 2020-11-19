using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.UserAggregate;
using Infrastructure.Persistence;
using Infrastructure.Repositories.EFCore.Common.BaseRepository;
using Infrastructure.Repositories.EFCore.UserRepositories.Contracts;
using Utility;

namespace Infrastructure.Repositories.EFCore.UserRepositories.Repositories
{
   public class UserRoleRepository:Repository<UserRole>,IUserRoleRepository,IScopedDependency
    {
        public UserRoleRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public void RemoveUserRoles(IEnumerable<UserRole> userRoles)
        {
            base.DeleteRange(userRoles);
        }
    }
}
