using Domain.UserAggregate;

namespace Infrastructure.Repositories.EFCore.Contracts
{
    public interface IUserRepository:IRepository<User>
    {
    }
}
