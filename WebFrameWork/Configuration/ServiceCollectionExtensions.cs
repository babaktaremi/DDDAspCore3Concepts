﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Services.Identity.Manager;
using Infrastructure.Persistence;
using Infrastructure.Repositories.EFCore.Contracts;
using MediatR;
using MediatR.Pipeline;
using Utility;
using Utility.Exceptions;
using Utility.Utilities;

namespace WebFramework.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options
                    .UseSqlServer(configuration.GetConnectionString("SqlServer"));
                    //Tips
                    //Automatic client evaluation is no longer supported. This event is no longer generated.
                    //This line is no longer needed.
                    //.ConfigureWarnings(warning => warning.Throw(RelationalEventId.QueryClientEvaluationWarning));
            });
        }

        public static void AddMinimalMvc(this IServiceCollection services)
        {
            //https://github.com/aspnet/AspNetCore/blob/0303c9e90b5b48b309a78c2ec9911db1812e6bf3/src/Mvc/Mvc/src/MvcServiceCollectionExtensions.cs
            services.AddControllers(options =>
            {
               // options.Filters.Add(new AuthorizeFilter()); //Apply AuthorizeFilter as global filter to all actions

                //Like [ValidateAntiforgeryToken] attribute but dose not validatie for GET and HEAD http method
                //You can ingore validate by using [IgnoreAntiforgeryToken] attribute
                //Use this filter when use cookie 
                //options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());

                //options.UseYeKeModelBinder();
            }).AddNewtonsoftJson(/*option =>
            {
                option.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                option.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            }*/);

            #region Old way (We don't need Muhammad from ASP.NET Core 3.0 onwards)
            ////https://github.com/aspnet/Mvc/blob/release/2.2/src/Microsoft.AspNetCore.Mvc/MvcServiceCollectionExtensions.cs
            //services.AddMvcCore(options =>
            //{
            //    options.Filters.Add(new AuthorizeFilter());

            //    //Like [ValidateAntiforgeryToken] attribute but dose not validatie for GET and HEAD http method
            //    //You can ingore validate by using [IgnoreAntiforgeryToken] attribute
            //    //Use this filter when use cookie 
            //    //options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());

            //    //options.UseYeKeModelBinder();
            //})
            //.AddApiExplorer()
            //.AddAuthorization()
            //.AddFormatterMappings()
            //.AddDataAnnotations()
            //.AddJsonOptions(option =>
            //{
            //    //option.JsonSerializerOptions
            //})
            //.AddNewtonsoftJson(/*option =>
            //{
            //    option.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            //    option.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            //}*/)

            ////Microsoft.AspNetCore.Mvc.Formatters.Json
            ////.AddJsonFormatters(/*options =>
            ////{
            ////    options.Formatting = Newtonsoft.Json.Formatting.Indented;
            ////    options.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            ////}*/)

            //.AddCors()
            //.SetCompatibilityVersion(CompatibilityVersion.Latest); //.SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            #endregion
        }

        public static void AddJwtAuthentication(this IServiceCollection services, JwtSettings jwtSettings)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var secretkey = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);
                var encryptionkey = Encoding.UTF8.GetBytes(jwtSettings.Encryptkey);

                var validationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero, // default: 5 min
                    RequireSignedTokens = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretkey),

                    RequireExpirationTime = true,
                    ValidateLifetime = true,

                    ValidateAudience = true, //default : false
                    ValidAudience = jwtSettings.Audience,

                    ValidateIssuer = true, //default : false
                    ValidIssuer = jwtSettings.Issuer,

                    TokenDecryptionKey = new SymmetricSecurityKey(encryptionkey)
                };

                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = validationParameters;
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        //var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                        //logger.LogError("Authentication failed.", context.Exception);

                        if (context.Exception != null)
                            throw new AppException(ApiResultStatusCode.UnAuthorized, "Authentication failed.", HttpStatusCode.Unauthorized, context.Exception, null);

                        return Task.CompletedTask;
                    },
                    OnTokenValidated = async context =>
                    {
                        var signInManager = context.HttpContext.RequestServices.GetRequiredService<AppSignInManager>();
                        var userRepository = context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();

                        var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                        if (claimsIdentity.Claims?.Any() != true)
                            context.Fail("This token has no claims.");

                        var securityStamp = claimsIdentity.FindFirstValue(new ClaimsIdentityOptions().SecurityStampClaimType);
                        if (!securityStamp.HasValue())
                            context.Fail("This token has no secuirty stamp");

                        //Find user and token from database and perform your custom validation
                        var userId = claimsIdentity.GetUserId<int>();
                        var user = await userRepository.GetByIdAsync(context.HttpContext.RequestAborted, userId);

                        //if (user.SecurityStamp != Guid.Parse(securityStamp))
                        //    context.Fail("Token secuirty stamp is not valid.");

                        var validatedUser = await signInManager.ValidateSecurityStampAsync(context.Principal);
                        if (validatedUser == null)
                            context.Fail("Token secuirty stamp is not valid.");

                    },
                    OnChallenge = context =>
                    {
                        //var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                        //logger.LogError("OnChallenge error", context.Error, context.ErrorDescription);

                        if (context.AuthenticateFailure != null)
                            throw new AppException(ApiResultStatusCode.UnAuthorized, "Authenticate failure.", HttpStatusCode.Unauthorized, context.AuthenticateFailure, null);
                        throw new AppException(ApiResultStatusCode.UnAuthorized, "You are unauthorized to access this resource.", HttpStatusCode.Unauthorized);

                        //return Task.CompletedTask;
                    }
                };
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
            services.AddScoped(typeof(IRequestPostProcessor<,>), typeof(CommitCommandPostProcessor<,>));
        }
    }
}
