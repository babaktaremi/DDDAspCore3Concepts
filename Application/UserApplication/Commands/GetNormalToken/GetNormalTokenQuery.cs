using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Services.Jwt;
using MediatR;

namespace Application.UserApplication.Commands.GetNormalToken
{
   public class GetNormalTokenQuery:IRequest<OperationResult<AccessToken>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
