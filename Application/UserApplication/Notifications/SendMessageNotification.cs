using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.UserApplication.Notifications
{
   public class SendMessageNotification:INotification
    {
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
