using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using MediatR;

namespace Application.UserApplication.Queries.GenerateTwoFactorAuthenticator
{
   public class GenerateTwoFactorAuthenticatorQuery:IRequest<OperationResult<GenerateTwoFactorAuthenticatorQueryResult>>
    {
       
    }
}
