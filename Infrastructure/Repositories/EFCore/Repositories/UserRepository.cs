using Domain.UserAggregate;
using Infrastructure.Persistence;
using Infrastructure.Repositories.EFCore.Contracts;
using Utility;

namespace Infrastructure.Repositories.EFCore.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository, IScopedDependency
    {


        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }


    }
}
