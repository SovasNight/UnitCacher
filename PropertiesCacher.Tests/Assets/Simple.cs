#pragma warning disable IDE1006

namespace PropertiesCacher.Tests.Assets
{
    // Assets of types for using as Property Type

    public class SimpleClass
    {
        public int Prop { get; set; }
        public SimpleClass(int value) => this.Prop = value;
    }

    public struct SimpleStruct
    {
        public int Prop { get; set; }

        public SimpleStruct(int value) => this.Prop = value;
    }

    public interface SimpleInterface
    {
        public int Prop { get; set; }
    }
}
