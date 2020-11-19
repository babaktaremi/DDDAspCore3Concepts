using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using MediatR;

namespace Application.AdminApplication.Queries.GetAllAdminsQuery
{
   public class GetAllAdminsQuery:IRequest<OperationResult<GetAllAdminQueryResult>>
    {
        public int PageNumber { get; set; }
        public int Take { get; set; }
        public int Skip => (PageNumber-1) * Take;
    }
}
