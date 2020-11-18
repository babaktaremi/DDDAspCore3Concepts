using Domain.UserAggregate;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Utility;

namespace Application.Services.Identity.Store
{
    public class RoleStore:RoleStore<Role,ApplicationDbContext,int,UserRole,RoleClaim>
    {
        public RoleStore(ApplicationDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
}
