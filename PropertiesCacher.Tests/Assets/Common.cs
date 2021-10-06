﻿namespace PropertiesCacher.Tests.Assets
{
    // Assets for tests

    public class TestClass
    {
        public int IntProp { get; set; }
        public int GetOnlyProp { get; }
        public virtual int VirtualProp { get; set; }
        public SimpleClass ClassProp { get; set; }
        public SimpleStruct StructProp { get; set; }
        public SimpleInterface InterfaceProp { get; set; }
        public SimpleAbstract AbstractProp { get; set; }

        public TestClass(int value) {
            this.IntProp = value;
            this.GetOnlyProp = value;
            this.VirtualProp = value;
            this.ClassProp = new SimpleClass(value);
            this.StructProp = new SimpleStruct(value);
            this.InterfaceProp = new TestInterfaceImp(value);
            this.AbstractProp = new TestAbstractImp(value);
        }
    }

    public class TestInterfaceImp : SimpleInterface
    {
        public int Prop { get; set; }
        public TestInterfaceImp(int value) => this.Prop = value;
    }

    public class TestAbstractImp : SimpleAbstract
    {
        public override int Prop { get; set; }
        public TestAbstractImp(int value) => this.Prop = value;
    }

    public class TestDescernant : TestClass
    {
        public override int VirtualProp { get => base.VirtualProp; set => base.VirtualProp = value; }
        public TestDescernant(int value) : base(value) { }
    }

    public class TestRefProperyClass
    {
        private int intValue;
        private SimpleStruct structValue;
        private SimpleClass classValue;
        private SimpleInterface interfaceValue;

        public ref int IntProp => ref intValue;
        public ref SimpleStruct StructProp => ref structValue;
        public ref SimpleClass ClassProp => ref classValue;
        public ref SimpleInterface InterfaceProp => ref interfaceValue;

        public TestRefProperyClass(int value) {
            this.intValue = value;
            this.structValue = new SimpleStruct(value);
            this.classValue = new SimpleClass(value);
            this.interfaceValue = new TestInterfaceImp(value);
        }
    }
}
