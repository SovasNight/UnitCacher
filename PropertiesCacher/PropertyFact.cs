using System;
using System.Collections.Generic;
using System.Text;

namespace PropertiesCacher
{
    /// <summary>
    /// Abstract class define accessors methods for delegated property.
    /// Storing base information about Property.
    /// </summary>
    public abstract class PropertyFact : PropertyAccessor
    {
        public string PropertyName { get; }
        public Type PropertyType { get; }
        public Type DeclaringType { get; }

        protected PropertyFact(string propertyName, Type propertyType, Type declaringType) {
            this.PropertyName = propertyName;
            this.PropertyType = propertyType;
            this.DeclaringType = declaringType;
        }
    }
}
