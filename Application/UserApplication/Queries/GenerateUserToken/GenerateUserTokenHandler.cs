using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Services.Identity.Manager;
using Application.Services.Jwt;
using Application.UserApplication.Queries.GenerateUserToken.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.UserApplication.Queries.GenerateUserToken
{
   public class GenerateUserTokenHandler:IRequestHandler<GenerateUserTokenQuery,OperationResult<GenerateTokenQueryResult>>
   {
       private readonly IJwtService _jwtService;
       private readonly AppUserManager _userManager;
       private readonly AppSignInManager _signInManager;
       

       public GenerateUserTokenHandler(IJwtService jwtService, AppUserManager userManager, AppSignInManager signInManager)
       {
           _jwtService = jwtService;
           _userManager = userManager;
           _signInManager = signInManager;
       }

       public async Task<OperationResult<GenerateTokenQueryResult>> Handle(GenerateUserTokenQuery request, CancellationToken cancellationToken)
       {
           var user = await _userManager.FindByNameAsync(request.UserName);

           if(user is null)
               return OperationResult<GenerateTokenQueryResult>.FailureResult("User Not Found");

           var passwordValidator = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

           if(passwordValidator == PasswordVerificationResult.Failed)
               return OperationResult<GenerateTokenQueryResult>.FailureResult("User Password Incorrect");


           user.UserLoggedIn();


                return OperationResult<GenerateTokenQueryResult>.SuccessResult(new GenerateTokenQueryResult
                {UserKey = user.GeneratedCode
                });
       }
    }
}
