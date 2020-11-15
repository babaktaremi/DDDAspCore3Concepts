using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.UserAggregate;

namespace Infrastructure.Repositories.EFCore.UserRepositories.Contracts
{
   public interface IRefreshTokenRepository
   {
       Task<Guid> CreateToken(int userId);
       Task<UserRefreshToken> GetTokenWithInvalidation(Guid id);
       Task<User> GetUserByRefreshToken(Guid tokenId);
   }
}
