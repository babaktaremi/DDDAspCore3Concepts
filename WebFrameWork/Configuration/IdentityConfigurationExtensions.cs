using Application.Services.Identity;
using Application.Services.Identity.Extensions;
using Application.Services.Identity.Manager;
using Application.Services.Identity.PermissionManager;
using Application.Services.Identity.Store;
using Application.Services.Identity.validator;
using Domain.UserAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace WebFrameWork.Configuration
{
    public static class IdentityConfigurationExtensions
    {
        public static void AddCustomIdentity(this IServiceCollection services)
        {
            services.AddScoped<IUserValidator<User>, AppUserValidator>();
            services.AddScoped<UserValidator<User>, AppUserValidator>();

            services.AddScoped<IUserClaimsPrincipalFactory<User>, AppUserClaimsPrincipleFactory>();

            services.AddScoped<IRoleValidator<Role>, AppRoleValidator>();
            services.AddScoped<RoleValidator<Role>, AppRoleValidator>();

            services.AddScoped<IAuthorizationHandler, DynamicPermissionHandler>();
            services.AddScoped<IDynamicPermissionService, DynamicPermissionService>();
           
            //For [ProtectPersonalData] Attribute In Identity

            //services.AddScoped<ILookupProtectorKeyRing, KeyRing>();

            //services.AddScoped<ILookupProtector, LookupProtector>();

            //services.AddScoped<IPersonalDataProtector, PersonalDataProtector>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(ConstantPolicies.DynamicPermission, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.Requirements.Add(new DynamicPermissionRequirement());
                });
            });

            services.AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Stores.ProtectPersonalData = false;

                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = true;


                //options.Stores.ProtectPersonalData = true;



            }).AddUserStore<AppUserStore>()
                .AddRoleStore<RoleStore>().
                //.AddUserValidator<AppUserValidator>().
                //AddRoleValidator<AppRoleValidator>().
                AddUserManager<AppUserManager>().
                AddRoleManager<AppRoleManager>().
                AddErrorDescriber<AppErrorDescriber>()
                // .AddClaimsPrincipalFactory<AppUserClaimsPrincipleFactory>()
                .AddDefaultTokenProviders().
            AddSignInManager<AppSignInManager>()
                .AddPasswordlessLoginTotpTokenProvider();

        }
    }
}
