using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Admin.Model.RoleViewModel
{
    public class RolePermissionsViewModel
    {
        public string RouteName { get; set; }
        public string RouteValue { get; set; }
        public int RoleId { get; set; }
        public bool HasPermission { get; set; }
    }
}
