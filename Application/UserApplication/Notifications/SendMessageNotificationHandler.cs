using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UserApplication.Notifications
{
   public class SendMessageNotificationHandler:INotificationHandler<SendMessageNotification>
   {
       private readonly ILogger<SendMessageNotificationHandler> _logger;

       public SendMessageNotificationHandler(ILogger<SendMessageNotificationHandler> logger)
       {
           _logger = logger;
       }

        public Task Handle(SendMessageNotification notification, CancellationToken cancellationToken)
        {
            _logger.LogWarning($"Sending Email To {notification.Email} with message {notification.Message}");
            return Task.CompletedTask;
        }
    }
}
