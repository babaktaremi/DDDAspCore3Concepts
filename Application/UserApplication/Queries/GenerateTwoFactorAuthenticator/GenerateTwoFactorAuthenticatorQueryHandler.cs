using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Services.Identity.Manager;
using Domain.UserAggregate;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.UserApplication.Queries.GenerateTwoFactorAuthenticator
{
   public class GenerateTwoFactorAuthenticatorQueryHandler:IRequestHandler<GenerateTwoFactorAuthenticatorQuery,OperationResult<GenerateTwoFactorAuthenticatorQueryResult>>
   {
       private readonly AppUserManager _userManager;
       private readonly IHttpContextAccessor _httpContext;

       public GenerateTwoFactorAuthenticatorQueryHandler(AppUserManager userManager, IHttpContextAccessor httpContext)
       {
           _userManager = userManager;
           _httpContext = httpContext;
       }

        public async Task<OperationResult<GenerateTwoFactorAuthenticatorQueryResult>> Handle(GenerateTwoFactorAuthenticatorQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetUserAsync(_httpContext.HttpContext.User);

            var authenticatorKey = await _userManager.GetAuthenticatorKeyAsync(user);

            if (authenticatorKey == null)
            {
                await _userManager.ResetAuthenticatorKeyAsync(user);
                authenticatorKey = await _userManager.GetAuthenticatorKeyAsync(user);
            }

            return OperationResult<GenerateTwoFactorAuthenticatorQueryResult>.SuccessResult(new GenerateTwoFactorAuthenticatorQueryResult{Token = authenticatorKey});
        }
    }
}
