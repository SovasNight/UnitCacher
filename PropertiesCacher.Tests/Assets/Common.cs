namespace PropertiesCacher.Tests.Assets
{
    // Assets for tests

    public class TestClass
    {
        public int IntProp { get; set; }
        public int GetOnlyProp { get; }
        public SimpleClass ClassProp { get; set; }
        public SimpleStruct StructProp { get; set; }
        public SimpleInterface InterfaceProp { get; set; }

        public TestClass(int value) {
            this.IntProp = value;
            this.GetOnlyProp = value;
            this.ClassProp = new SimpleClass(value);
            this.StructProp = new SimpleStruct(value);
            this.InterfaceProp = new TestInterfaceImp(value);
        }
    }

    public class TestInterfaceImp : SimpleInterface
    {
        public int Prop { get; set; }
        public TestInterfaceImp(int value) => this.Prop = value;
    }
}
