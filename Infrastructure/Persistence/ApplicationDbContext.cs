using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Domain.Common;
using Domain.UserAggregate;
using Infrastructure.EventDispatchers.EventDispatchHandlers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Utility.Utilities;

namespace Infrastructure.Persistence
{


    public class ApplicationDbContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        private readonly IEventDispatchHandler _dispatchHandler;
        public ApplicationDbContext(DbContextOptions options, IEventDispatchHandler dispatchHandler)
            : base(options)
        {
            _dispatchHandler = dispatchHandler;
        }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            base.OnModelCreating(modelBuilder);

            var entitiesAssembly = typeof(IEntity).Assembly;
            modelBuilder.RegisterAllEntities<IEntity>(entitiesAssembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            modelBuilder.AddRestrictDeleteBehaviorConvention();
            modelBuilder.AddPluralizingTableNameConvention();


        }

        //TODO : Replace Save Change Override with interceptors instead
        public override int SaveChanges()
        {
            _cleanString();
            var result= base.SaveChanges();
            _handleDomainEvents();
            return result;
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            _cleanString();
            var result= base.SaveChanges(acceptAllChangesOnSuccess);
            _handleDomainEvents();
            return result;
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            _cleanString();
            var result= base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            _handleDomainEvents();
            return result;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            _cleanString();
            var result= base.SaveChangesAsync(cancellationToken);
            _handleDomainEvents();
            return result;
        }

        private void _cleanString()
        {
            var changedEntities = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);
            foreach (var item in changedEntities)
            {
                if (item.Entity == null)
                    continue;

                var properties = item.Entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(string));

                foreach (var property in properties)
                {
                    var propName = property.Name;
                    var val = (string)property.GetValue(item.Entity, null);

                    if (val.HasValue())
                    {
                        var newVal = val.Fa2En().FixPersianChars();
                        if (newVal == val)
                            continue;
                        property.SetValue(item.Entity, newVal, null);
                    }
                }
            }
        }

        private void _handleDomainEvents()
        {
            List<IAggregateRoot> entities = ChangeTracker.Entries().Where(x => x.Entity is IAggregateRoot)
                .Select(x => (IAggregateRoot) x.Entity).ToList();

            foreach (var domainEntity in entities)
            {
                _dispatchHandler.HandleEvents(domainEntity.DomainEvents);
                domainEntity.ClearDomainEvents();
            }
        }
    }
}
