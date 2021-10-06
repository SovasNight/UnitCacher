namespace PropertiesCacher
{
    /// <summary>
    /// Abstract class define accessors methods for delegated property.
    /// </summary>
    public abstract class PropertyAccessor
    {
        /// <summary>
        /// Gets a boxed value from an instance property.
        /// </summary>
        /// <param name="instance">The instance which type declares this property.</param>
        /// <returns>The boxed property value.</returns>
        public abstract object Get_Value(object instance);

        /// <summary>
        /// Sets a boxed value to an instance property.
        /// </summary>
        /// <param name="instance">The instance which type declares this property.</param>
        /// <param name="value">The boxed value to be set.</param>
        public abstract void Set_Value(object instance, object value);
    }
}
