using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Services.Jwt;
using MediatR;

namespace Application.UserApplication.Queries.ConfirmTwoFactor
{
   public class ConfirmTwoFactorQuery:IRequest<OperationResult<AccessToken>>
    {
        public string UserKey { get; set; }
        public string Code { get; set; }
    }
}
