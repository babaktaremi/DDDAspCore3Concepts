using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.MapperConfiguration;
using Domain.UserAggregate;

namespace Application.Services.Identity.Dtos
{
   public class GetRolesDto:ICreateMapper<Role>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
