using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Repositories.EFCore.UserRepositories.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.EventDispatchers.EventDispatchServices.UserEventServices.UserLoginEventHandler
{
   public class UserLoginEventWorker:BackgroundService
   {
       private readonly IServiceProvider _serviceProvider;
       private readonly ILogger<UserLoginEventWorker> _logger;
       public UserLoginEventWorker(IServiceProvider serviceProvider, ILogger<UserLoginEventWorker> logger)
       {
           _serviceProvider = serviceProvider;
           _logger = logger;
       }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var scope = _serviceProvider.CreateScope();

                    var channelService = scope.ServiceProvider.GetService<IUserLoginChannel>();
                    var refreshTokenRepository = scope.ServiceProvider.GetService<IRefreshTokenRepository>();

                    await foreach (var item in channelService.ReturnValue(stoppingToken))
                    {
                        await refreshTokenRepository.RemoveUserOldTokens(item, stoppingToken);
                        _logger.LogInformation("User with {item} ID old tokens has been removed",item);
                    }

                }
                catch (Exception e)
                {
                   _logger.LogError(e,e.Message);
                }
               
            }
        }
    }
}
