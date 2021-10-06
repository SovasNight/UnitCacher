using System;
using System.Reflection;

using Xunit;

#pragma warning disable IDE0008

namespace PropertiesCacher.Tests
{
    public class PropertyAccessorsFactoryTests
    {
        [Fact]
        public void CreateAccessor_ClassIntProp_Get_Value_100() {
            PropertyInfo pi = typeof(Assets.TestClass).GetProperty("IntProp");
            var instance = new Assets.TestClass(100);
            var expect = instance.IntProp;

            PropertyAccessor accessor = new PropertyAccessorsFactory().CreateAccessor(pi);
            object result = accessor.Get_Value(instance);

            Assert.Equal(expect, result);
        }

        [Fact]
        public void CreateAccessor_ClassIntProp_Set_Value_100() {
            PropertyInfo pi = typeof(Assets.TestClass).GetProperty("IntProp");
            var instance = new Assets.TestClass(0);
            var expect = 100;

            PropertyAccessor accessor = new PropertyAccessorsFactory().CreateAccessor(pi);
            accessor.Set_Value(instance, expect);

            object result = instance.IntProp;
            Assert.Equal(expect, result);
        }

        [Fact]
        public void CreateAccessor_ClassGetOnyProp_Set_Value_Exception() {
            PropertyInfo pi = typeof(Assets.TestClass).GetProperty("GetOnlyProp");
            var instance = new Assets.TestClass(0);

            PropertyAccessor accessor = new PropertyAccessorsFactory().CreateAccessor(pi);
            Action action = () => accessor.Set_Value(instance, 0);

            Assert.Throws<NotImplementedException>(action);
        }

        [Fact]
        public void CreateAccessor_ClassClassProp_Get_Value_SimpleClass_100() {
            PropertyInfo pi = typeof(Assets.TestClass).GetProperty("ClassProp");
            var instance = new Assets.TestClass(100);
            var expect = instance.ClassProp;

            PropertyAccessor accessor = new PropertyAccessorsFactory().CreateAccessor(pi);
            object result = accessor.Get_Value(instance);

            Assert.Equal(expect, result);
        }

        [Fact]
        public void CreateAccessor_ClassClassProp_Set_Value_SimpleClass_100() {
            PropertyInfo pi = typeof(Assets.TestClass).GetProperty("ClassProp");
            var instance = new Assets.TestClass(0);
            var expect = new Assets.SimpleClass(100);

            PropertyAccessor accessor = new PropertyAccessorsFactory().CreateAccessor(pi);
            accessor.Set_Value(instance, expect);

            object result = instance.ClassProp;
            Assert.Equal(expect, result);
        }

        [Fact]
        public void CreateAccessor_ClassStructProp_Get_Value_SimpleStruct_100() {
            PropertyInfo pi = typeof(Assets.TestClass).GetProperty("StructProp");
            var instance = new Assets.TestClass(100);
            var expect = instance.StructProp;

            PropertyAccessor accessor = new PropertyAccessorsFactory().CreateAccessor(pi);
            object result = accessor.Get_Value(instance);

            Assert.Equal(expect, result);
        }

        [Fact]
        public void CreateAccessor_CalssStructProp_Set_Value_SimpleStruct_100() {
            PropertyInfo pi = typeof(Assets.TestClass).GetProperty("StructProp");
            var instance = new Assets.TestClass(0);
            var expect = new Assets.SimpleStruct(100);

            PropertyAccessor accessor = new PropertyAccessorsFactory().CreateAccessor(pi);
            accessor.Set_Value(instance, expect);

            object result = instance.StructProp;
            Assert.Equal(expect, result);
        }

        [Fact]
        public void CreateAccessor_ClassInterfaceProp_Get_Value_TestInterfaceImp_100() {
            PropertyInfo pi = typeof(Assets.TestClass).GetProperty("InterfaceProp");
            var instance = new Assets.TestClass(100);
            var expect = instance.InterfaceProp;

            PropertyAccessor accessor = new PropertyAccessorsFactory().CreateAccessor(pi);
            object result = accessor.Get_Value(instance);

            Assert.Equal(expect, result);
        }

        [Fact]
        public void CreateAccessor_ClassInterfaceProp_Set_Value_TestInterfaceImp_100() {
            PropertyInfo pi = typeof(Assets.TestClass).GetProperty("InterfaceProp");
            var instance = new Assets.TestClass(0);
            var expect = new Assets.TestInterfaceImp(100);

            PropertyAccessor accessor = new PropertyAccessorsFactory().CreateAccessor(pi);
            accessor.Set_Value(instance, expect);

            object result = instance.InterfaceProp;
            Assert.Equal(expect, result);
        }
    }
}
