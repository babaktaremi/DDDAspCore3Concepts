using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.UserAggregate;
using Infrastructure.Persistence;
using Infrastructure.Repositories.EFCore.Common.BaseRepository;
using Infrastructure.Repositories.EFCore.UserRepositories.Contracts;
using Infrastructure.Repositories.EFCore.UserRepositories.Dtos;
using Microsoft.EntityFrameworkCore;
using Utility;

namespace Infrastructure.Repositories.EFCore.UserRepositories.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository, IScopedDependency
    {
        private readonly IMapper _mapper;
        
        public UserRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _mapper = mapper;
        }


        public Task<List<GetUserNamesDto>> GetUserNames()
        {
            var userNames= base.TableNoTracking.ProjectTo<GetUserNamesDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return userNames;
        }

        public Task<User> GetUserWithOrders(int userId)
        {
            return base.TableNoTracking.Include(c => c.Orders.Where(c => c.UserId == userId))
                .Where(c => c.Id == userId).FirstOrDefaultAsync();
        }

        public void DeleteAdminUser(User user)
        {
            if (user.UserRoles.Any())
            {
                base.Delete(user);
            }
        }
    }
}
