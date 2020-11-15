using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.UserAggregate
{
   public class UserRefreshToken:BaseEntity<Guid>
    {
        public UserRefreshToken()
        {
            CreatedAt=DateTime.Now;
        }

        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsValid { get; set; }
    }
}
