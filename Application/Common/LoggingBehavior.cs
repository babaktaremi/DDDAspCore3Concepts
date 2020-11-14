using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Common
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }


        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            //_logger.LogInformation($"Handling {typeof(TRequest).Name}");
            
            //_logger.LogInformation($"Handled {typeof(TResponse).Name}");

            try
            {
                var response = await next();
                return response;
            }
            catch (Exception e)
            {
               _logger.LogError(e,e.Message);
               return default(TResponse);
            }

          
        }
    }
}
