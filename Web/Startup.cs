using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Application.Common;
using Application.Services.Jwt;
using Application.UserApplication.Commands.Create.Model;
using Autofac;
using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Utility;
using Utility.MapperConfiguration;
using Web.Controllers;
using WebFrameWork.Configuration;
using WebFrameWork.Filters;
using WebFrameWork.Middlewares;
using WebFrameWork.Swagger;

namespace Web
{
    public class Startup
    {
        private readonly SiteSettings _siteSetting;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            _siteSetting = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<SiteSettings>(Configuration.GetSection(nameof(SiteSettings)));

            services.AddDbContext(Configuration);

            services.AddCustomIdentity();

            services.AddHttpContextAccessor();

            services.AddJwtAuthentication(_siteSetting.JwtSettings);

            services.AddCustomApiVersioning();

            services.AddSwagger();

            services.AddMediatR(typeof(OperationResult<>).GetTypeInfo().Assembly);

            services.AddMediatRServices();

            services.AddAutoMapper(typeof(JwtService),typeof(AccountController));

            services.AddControllers(options =>
                {
                    options.Filters.Add(typeof(OkResultAttribute));
                    options.Filters.Add(typeof(NotFoundResultAttribute));
                    options.Filters.Add(typeof(ContentResultFilterAttribute));
                    options.Filters.Add(typeof(ModelStateValidationAttribute));
                    options.Filters.Add(typeof(BadRequestResultFilterAttribute));

                }).ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                })
                .AddFluentValidation(cfg => { cfg.RegisterValidatorsFromAssemblyContaining<UserCreateCommand>();});

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseCustomExceptionHandler();
            }

           

            app.UseHsts();

            app.UseHttpsRedirection();


            app.UseSwaggerAndUI();

            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseCors();

            app.UseEndpoints(config =>
            {
                config.MapControllers();

            });

            //Using 'UseMvc' to configure MVC is not supported while using Endpoint Routing.
            //To continue using 'UseMvc', please set 'MvcOptions.EnableEndpointRouting = false' inside 'ConfigureServices'.
            //app.UseMvc();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            //configure auto fac here
            builder.AddServices();

            //...
        }
    }
}
