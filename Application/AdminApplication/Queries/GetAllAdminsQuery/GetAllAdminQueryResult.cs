using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AdminApplication.Queries.GetAllAdminsQuery
{
    public class GetAllAdminQueryResult
    {
        public class AdminInformation
        {
            public string UserName { get; set; }
            public string Role { get; set; }
            public int UserId { get; set; }
        }

        public List<AdminInformation> AdminInformations { get; set; }
        public int NumberOfAdmins { get; set; }
    }
}
