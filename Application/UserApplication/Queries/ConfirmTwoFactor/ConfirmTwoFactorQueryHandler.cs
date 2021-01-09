using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Services.Identity.Manager;
using Application.Services.Jwt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UserApplication.Queries.ConfirmTwoFactor
{
    class ConfirmTwoFactorQueryHandler:IRequestHandler<ConfirmTwoFactorQuery,OperationResult<AccessToken>>
    {
        private readonly AppUserManager _userManager;
        private readonly IJwtService _jwtService;


        public ConfirmTwoFactorQueryHandler(AppUserManager userManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }


        public async Task<OperationResult<AccessToken>> Handle(ConfirmTwoFactorQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.GeneratedCode.Equals(request.UserKey), cancellationToken: cancellationToken);

            if(user is null)
                return OperationResult<AccessToken>.FailureResult("User Not Found");

            var isValid=await _userManager.VerifyTwoFactorTokenAsync(user, _userManager.Options.Tokens.AuthenticatorTokenProvider
                , request.Code);

            if (isValid)
            {
                var token = await _jwtService.GenerateAsync(user);

                return OperationResult<AccessToken>.SuccessResult(token);
            }

            return OperationResult<AccessToken>.FailureResult("Code is not valid");
        }
    }
}
