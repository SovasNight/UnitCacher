namespace PropertiesCacher.Tests.Assets
{
    public class SimpleClass
    {
        public int Prop { get; set; }
        public SimpleClass(int value) => this.Prop = value;
    }

    public class GetOnyProp
    {
        public int Prop { get; }
        public GetOnyProp(int value) => this.Prop = value;
    }

    public class SimpleObjectProp
    {
        public SimpleClass Prop { get; set; }
        public SimpleObjectProp(int value) => this.Prop = new SimpleClass(value);
    }

    public class SimpleStructProp
    {
        public SimpleStruct Prop { get; set; }
        public SimpleStructProp(int value) => this.Prop = new SimpleStruct(100);
    }

    public struct SimpleStruct
    {
        public int Prop { get; set; }

        public SimpleStruct(int value) => this.Prop = value;
    }
}
