using System;
using System.Collections.Generic;
using System.Linq;

namespace StockExchangeAnalyzer.Common.Domain.Model
{
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object otherObject)
        {
            var otherValueObject = otherObject as ValueObject;
            return otherValueObject != null 
                && GetEqualityComponents().SequenceEqual(otherValueObject.GetEqualityComponents()) 
                || ReferenceEquals(this, otherValueObject);
        }

        public override int GetHashCode()
        {
            var equalityComponents = GetEqualityComponents();
            var hash = 17;
            foreach (var component in equalityComponents)
                hash = hash * 23 + (component != null ? component.GetHashCode() : 0);
            return hash;
        }
    }
}
