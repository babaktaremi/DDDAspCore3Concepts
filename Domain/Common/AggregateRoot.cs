using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public interface IAggregateRoot
    {
         IReadOnlyList<IDomainEvent> DomainEvents { get;}
        void RaiseDomainEvent(IDomainEvent domainEvent);
        void ClearDomainEvents();
    }


   public abstract class AggregateRoot<TKey>:BaseEntity<TKey>, IAggregateRoot
    {
        [NotMapped]
        private readonly IList<IDomainEvent> _domainEvents = new List<IDomainEvent>();

        //[NotMapped]
        //public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.ToList();

        [NotMapped]
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.ToList();

        public void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }

   public abstract class AggregateRoot : AggregateRoot<int> { }
}
