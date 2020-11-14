using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.UserAggregate;
using Infrastructure.Repositories.EFCore.UserRepositories.Dtos;

namespace Infrastructure.Repositories.EFCore.UserRepositories.Contracts
{
    public interface IUserRepository
    {
        Task<List<GetUserNamesDto>> GetUserNames();
    }
}
