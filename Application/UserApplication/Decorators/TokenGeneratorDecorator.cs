using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Services.Jwt;
using Application.UserApplication.Queries.GenerateUserToken.Model;
using MediatR;
using Microsoft.Extensions.Logging;
using Utility;

namespace Application.UserApplication.Decorators
{
    public class TokenGeneratorDecorator : IPipelineBehavior<GenerateUserTokenQuery, OperationResult<AccessToken>>,IScopedDependency
    {
        private readonly ILogger<TokenGeneratorDecorator> _logger;

        public TokenGeneratorDecorator(ILogger<TokenGeneratorDecorator> logger)
        {
            _logger = logger;
        }

        public async Task<OperationResult<AccessToken>> Handle(GenerateUserTokenQuery request, CancellationToken cancellationToken, RequestHandlerDelegate<OperationResult<AccessToken>> next)
        {
            _logger.LogWarning("Generating Token");
            var result = await next();
            _logger.LogWarning("Token Generated");

            return result;
        }
    }
}
