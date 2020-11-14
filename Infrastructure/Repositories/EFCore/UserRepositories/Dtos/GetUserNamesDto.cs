using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.UserAggregate;
using Infrastructure.Repositories.Common.MapperConfiguration;

namespace Infrastructure.Repositories.EFCore.UserRepositories.Dtos
{
   public class GetUserNamesDto:ICreateMapper<User>
    {
        public string UserName { get; set; }
    }
}
