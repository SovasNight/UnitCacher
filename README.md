# UnitCacher
Provides classes for caching .net units.

Requers `.net Standart 2.1` (Tests `.net Core 3.1` )
## PropertiesCacher
Provides classes for caching Class Properties

### Using
```C#
public class Program
{
    public void Main(string[] args)
    {
        // Create the factory
        PropertyAccessorsFactory factory = new PropertyAccessorsFactory();
        // Property to be cached
        PropertyInfo pi = typeof(TestClass).GetProperty("Prop");
        // Create accessors cache
        PropertyAccessor accessor = factory.CreateAccessor(pi);
        
        // Instance for modificate by accessor
        TestClass instance = new TestClass() { Prop = 100 };
        
        // Using accessor
        accessor.Set_Value(instance, 400); // now instance.Prop == 400
        
        object result = accessor.Get_Value(instance); // returns 400
    }
}

public class TestClass
{
    public Prop { get; set; }
}
```

### Not implemented now
Handling virtuals properties (Defined in class or interface).
Handing static properties.
Handing properties by ref.
Casting from assignable types.
Caching valueTypes properties.
