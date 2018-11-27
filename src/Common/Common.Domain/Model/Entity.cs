using System.Collections.Generic;
using System.Linq;

namespace StockExchangeAnalyzer.Common.Domain.Model
{
    public abstract class Entity
    {
        protected abstract IEnumerable<object> GetIdentityComponents();

        public override bool Equals(object otherObject)
        {
            var otherValueObject = otherObject as Entity;
            return otherValueObject != null
                && GetIdentityComponents().SequenceEqual(otherValueObject.GetIdentityComponents())
                || ReferenceEquals(this, otherValueObject);
        }

        public override int GetHashCode()
        {
            var equalityComponents = GetIdentityComponents();
            var hash = 17;
            foreach (var component in equalityComponents)
                hash = hash * 23 + (component != null ? component.GetHashCode() : 0);
            return hash;
        }
    }
}