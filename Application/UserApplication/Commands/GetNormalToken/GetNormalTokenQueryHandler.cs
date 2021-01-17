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

namespace Application.UserApplication.Commands.GetNormalToken
{
  public class GetNormalTokenQueryHandler:IRequestHandler<GetNormalTokenQuery,OperationResult<AccessToken>>
  {
      private readonly AppUserManager _userManager;
      private readonly IJwtService _jwtService;

      public GetNormalTokenQueryHandler(AppUserManager userManager, IJwtService jwtService)
      {
          _userManager = userManager;
          _jwtService = jwtService;
      }

        public async Task<OperationResult<AccessToken>> Handle(GetNormalTokenQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if(user is null)
                return OperationResult<AccessToken>.FailureResult("User Not Found");

            if(user.TwoFactorEnabled)
                return OperationResult<AccessToken>.FailureResult("Use Two Factor Login");

            var token = await _jwtService.GenerateAsync(user);

            return OperationResult<AccessToken>.SuccessResult(token);
        }
    }
}
