using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Services.Identity.Manager;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.UserApplication.Commands.RegisterAuthenticatorToken
{
   public class RegisterAuthenticatorCommandHandler:IRequestHandler<RegisterAuthenticatorCommand,OperationResult<bool>>
   {
       private readonly AppUserManager _userManager;
       private readonly IHttpContextAccessor _httpContext;

       public RegisterAuthenticatorCommandHandler(AppUserManager userManager, IHttpContextAccessor httpContext)
       {
           _userManager = userManager;
           _httpContext = httpContext;
       }

        public async Task<OperationResult<bool>> Handle(RegisterAuthenticatorCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetUserAsync(_httpContext.HttpContext.User);

            var isValid = await _userManager.VerifyTwoFactorTokenAsync(user,
                _userManager.Options.Tokens.AuthenticatorTokenProvider, request.Code);

            if (!isValid)
            {
                return OperationResult<bool>.FailureResult("Invalid Code");
            }

            await _userManager.SetTwoFactorEnabledAsync(user, true);
          return OperationResult<bool>.SuccessResult(true);
        }
    }
}
