using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Domain.Common
{
    public interface IEntity
    {
    }

    //public abstract class DomainEntity
    //{
    //    [NotMapped]
    //    private readonly IList<IDomainEvent> _domainEvents = new List<IDomainEvent>();

    //    [NotMapped]
    //    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.ToList();

    //    public void RaiseDomainEvent(IDomainEvent domainEvent)
    //    {
    //        _domainEvents.Add(domainEvent);
    //    }

    //    public void ClearDomainEvents()
    //    {
    //        _domainEvents.Clear();
    //    }
    //}

    public abstract class BaseEntity<TKey> : IEntity
    {
        public TKey Id { get; protected set; }

        public override bool Equals(object obj)
        {
            if (!(obj is BaseEntity<TKey> other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            return Id.Equals(other.Id);
        }

        public static bool operator ==(BaseEntity<TKey> a, BaseEntity<TKey> b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(BaseEntity<TKey> a, BaseEntity<TKey> b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().ToString() + Id).GetHashCode();
        }
    }

    public abstract class BaseEntity : BaseEntity<int>
    {
       
    }
}
