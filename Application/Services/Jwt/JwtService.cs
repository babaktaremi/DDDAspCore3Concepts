using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Identity.Manager;
using Domain.UserAggregate;
using Infrastructure.Repositories.EFCore.UserRepositories.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Utility;

namespace Application.Services.Jwt
{
    public class JwtService : IJwtService, IScopedDependency
    {
        private readonly SiteSettings _siteSetting;
        private readonly AppUserManager _userManager;
        private IUserClaimsPrincipalFactory<User> _claimsPrincipal;

        private readonly IRefreshTokenRepository _refreshTokenRepository;
        //private readonly AppUserClaimsPrincipleFactory claimsPrincipleFactory;

        public JwtService(IOptionsSnapshot<SiteSettings> siteSetting, AppUserManager userManager, IUserClaimsPrincipalFactory<User> claimsPrincipal, IRefreshTokenRepository refreshTokenRepository)
        {
            _siteSetting = siteSetting.Value;
            _userManager = userManager;
            _claimsPrincipal = claimsPrincipal;
            _refreshTokenRepository = refreshTokenRepository;
        }
        public async Task<AccessToken> GenerateAsync(User user)
        {
            var secretKey = Encoding.UTF8.GetBytes(_siteSetting.JwtSettings.SecretKey); // longer that 16 character
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

            var encryptionkey = Encoding.UTF8.GetBytes(_siteSetting.JwtSettings.Encryptkey); //must be 16 character
            var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionkey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

            //var certificate = new X509Certificate2("d:\\aaaa2.cer"/*, "P@ssw0rd"*/);
            //var encryptingCredentials = new X509EncryptingCredentials(certificate);

            var claims = await _getClaimsAsync(user);

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = _siteSetting.JwtSettings.Issuer,
                Audience = _siteSetting.JwtSettings.Audience,
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now.AddMinutes(0),
                Expires = DateTime.Now.AddMinutes(_siteSetting.JwtSettings.ExpirationMinutes),
                SigningCredentials = signingCredentials,
                EncryptingCredentials = encryptingCredentials,
                Subject = new ClaimsIdentity(claims)
            };

            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            //JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
            //JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateJwtSecurityToken(descriptor);

            var refreshToken = await _refreshTokenRepository.CreateToken(user.Id);

            //string encryptedJwt = tokenHandler.WriteToken(securityToken);

            return new AccessToken(securityToken,refreshToken.ToString());
        }

        public Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_siteSetting.JwtSettings.SecretKey)),
                ValidateLifetime = false,
                TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_siteSetting.JwtSettings.Encryptkey))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return Task.FromResult(principal);
        }

        public async Task<AccessToken> GenerateByPhoneNumberAsync(string phoneNumber)
        {
            var user = await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
            var result = await this.GenerateAsync(user);
            return result;
        }

        public async Task<AccessToken> RefreshToken(string refreshTokenId)
        {
            var refreshToken = await _refreshTokenRepository.GetTokenWithInvalidation(Guid.Parse(refreshTokenId));

            if (refreshToken is null)
                return null;

            var user = await _refreshTokenRepository.GetUserByRefreshToken(Guid.Parse(refreshTokenId));

            if (user is null)
                return null;

            var result = await this.GenerateAsync(user);

            return result;
        }

        private async Task<IEnumerable<Claim>> _getClaimsAsync(User user)
        {
            var result = await _claimsPrincipal.CreateAsync(user);
            return result.Claims;



            //add custom claims
            //var list = new List<Claim>(result.Claims);
            //list.Add(new Claim(ClaimTypes.MobilePhone, "09123456987"));
            //return list;

            //JwtRegisteredClaimNames.Sub
            //var securityStampClaimType = new ClaimsIdentityOptions().SecurityStampClaimType;

            //var list = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Name, user.UserName),
            //    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            //    //new Claim(ClaimTypes.MobilePhone, "09123456987"),
            //   // new Claim(ClaimTypes., user.SecurityStamp.ToString())
            //};

            ////var roles = new Role[] { new Role { Name = "Admin" } };
            ////foreach (var role in roles)
            ////    list.Add(new Claim(ClaimTypes.Role, role.Name));

            //return list;
        }
    }
}
