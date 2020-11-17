using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.OrderApplication.Commands.Create
{
   public class OrderCreateCommandResult
    {
        public Guid OrderId { get; set; }
        public DateTime RegisteredDate { get; set; }
        public string State { get; set; }

    }
}
