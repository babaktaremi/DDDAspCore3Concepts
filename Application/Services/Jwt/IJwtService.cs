using System.Security.Claims;
using System.Threading.Tasks;
using Domain.UserAggregate;

namespace Application.Services.Jwt
{
    public interface IJwtService
    {
        Task<AccessToken> GenerateAsync(User user);
        Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token);
        Task<AccessToken> GenerateByPhoneNumberAsync(string phoneNumber);
    }
}