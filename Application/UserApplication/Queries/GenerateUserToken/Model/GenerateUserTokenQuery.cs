using Application.Common;
using Application.Services.Jwt;
using MediatR;

namespace Application.UserApplication.Queries.GenerateUserToken.Model
{
  public class GenerateUserTokenQuery:IRequest<OperationResult<AccessToken>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
