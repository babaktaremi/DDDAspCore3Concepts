using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using MediatR;

namespace Application.UserApplication.Commands.RegisterAuthenticatorToken
{
   public class RegisterAuthenticatorCommand:IRequest<OperationResult<bool>>
    {
        public string Code { get; set; }
    }
}
