# UnitCacher
Project represent the `.NET Standart 2.1` library for caching .net units.
All tests using `.NET Core 3.0` runtime.
## PropertiesCacher
Represent abstract class with factory to caching properties of 
classes, interfaces, structures.


For each sepecify property `PropertyAccessorsFactory.CreateAccessor(PropertyInfo pi)`
creates instance of dynamic type wich implement abstract class.


`PropertyAccessor` methods is delegate invokes to accessor methods of the Property for specify instance.
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