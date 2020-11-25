using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Services.Identity.Manager;
using Application.Services.Jwt;
using Application.UserApplication.Decorators;
using Application.UserApplication.Queries.GenerateUserToken.Model;
using Infrastructure.Persistence;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Utility;
using Utility.Utilities;
using WebFrameWork.Api;
using WebFrameWork.StatusCodeDescription;

namespace WebFrameWork.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options
                    .UseSqlServer(configuration.GetConnectionString("SqlServer"));
                    
            });
        }


        public static void AddCustomApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                //url segment => {version}
                options.AssumeDefaultVersionWhenUnspecified = true; //default => false;
                options.DefaultApiVersion = new ApiVersion(1, 0); //v1.0 == v1
                options.ReportApiVersions = true;

                //ApiVersion.TryParse("1.0", out var version10);
                //ApiVersion.TryParse("1", out var version1);
                //var a = version10 == version1;

                //options.ApiVersionReader = new QueryStringApiVersionReader("api-version");
                // api/posts?api-version=1

                //options.ApiVersionReader = new UrlSegmentApiVersionReader();
                // api/v1/posts

                //options.ApiVersionReader = new HeaderApiVersionReader(new[] { "Api-Version" });
                // header => Api-Version : 1

                //options.ApiVersionReader = new MediaTypeApiVersionReader()

                //options.ApiVersionReader = ApiVersionReader.Combine(new QueryStringApiVersionReader("api-version"), new UrlSegmentApiVersionReader())
                // combine of [querystring] & [urlsegment]
            });
        }

        public static void AddMediatRServices(this IServiceCollection services)
        {
            
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidateCommandBehavior<,>));
            //services
            //    .AddTransient<
            //        IPipelineBehavior<GenerateUserTokenQuery, OperationResult<AccessToken>>, TokenGeneratorDecorator>();
            //services.AddScoped(typeof(IRequestPostProcessor<,>), typeof(CommitCommandPostProcessor<,>));
        }

        public static void ConfigureSecurityStamp(this IServiceCollection services)
        {
            services.Configure<SecurityStampValidatorOptions>(o =>
            {
                o.ValidationInterval = TimeSpan.FromMinutes(10);
            });
        }
    }
}
