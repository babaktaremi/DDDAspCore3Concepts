using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Common.Marks;
using MediatR;

namespace Application.AdminApplication.Commands.DeleteAdmin
{
   public class DeleteAdminRequestCommand:IRequest<OperationResult<bool>>,ICommittable
    {
        public int UserId { get; set; }
    }
}
