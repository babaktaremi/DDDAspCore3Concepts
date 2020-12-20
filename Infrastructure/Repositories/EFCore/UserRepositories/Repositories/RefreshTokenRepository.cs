using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.UserAggregate;
using Infrastructure.Persistence;
using Infrastructure.Repositories.EFCore.Common.BaseRepository;
using Infrastructure.Repositories.EFCore.UserRepositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Utility;

namespace Infrastructure.Repositories.EFCore.UserRepositories.Repositories
{
    public class RefreshTokenRepository : Repository<UserRefreshToken>, IRefreshTokenRepository, IScopedDependency
    {
        public RefreshTokenRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Guid> CreateToken(int userId)
        {
            //var token = await base.Table.OrderByDescending(c => c.CreatedAt).Where(c => c.IsValid && c.UserId == userId).FirstOrDefaultAsync();

            //if (token !=null)
            //{
            //    return token.Id;
            //}

           var token = new UserRefreshToken { IsValid = true, UserId = userId };
            base.Add(token);

            await base.CommitAsync(CancellationToken.None);

            return token.Id;
        }

        public async Task<UserRefreshToken> GetTokenWithInvalidation(Guid id)
        {
            var token = await base.Table.Where(t => t.IsValid && t.Id.Equals(id)).FirstOrDefaultAsync();

            if (token == null) return null;
            token.IsValid = false;
            base.Update(token);
            await base.CommitAsync(CancellationToken.None);

            return token;
        }

        public async Task<User> GetUserByRefreshToken(Guid tokenId)
        {
            var user = await base.TableNoTracking.Include(t => t.User).Where(c => c.Id.Equals(tokenId))
                .Select(c => c.User).FirstOrDefaultAsync();

            return user;
        }

        public async Task RemoveUserOldTokens(int userId, CancellationToken cancellationToken)
        {
            var date = DateTime.Now.AddDays(-3);

            var tokens = await base.Table
                .Where(t => t.UserId == userId)
                .Where(t => !t.IsValid || t.CreatedAt <= date)
                .ToListAsync(cancellationToken: cancellationToken);

            if (tokens.Any())
            {
                base.DeleteRange(tokens);
                await base.CommitAsync(cancellationToken);
            }
        }
    }
}
