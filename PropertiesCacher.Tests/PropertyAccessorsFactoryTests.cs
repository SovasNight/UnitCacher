using System;
using System.Reflection;

using Xunit;

namespace PropertiesCacher.Tests
{
    public class PropertyAccessorsFactoryTests
    {
        [Fact]
        public void CreateAccessor_SimpleClass_Get_Value_100()
        {
            PropertyInfo pi = typeof(Assets.SimpleClass).GetProperty("Prop");
            PropertyAccessorsFactory factory = new PropertyAccessorsFactory();
            var instance = new Assets.SimpleClass(100);

            PropertyAccessor accessor = factory.CreateAccessor(pi);
            object result = accessor.Get_Value(instance);

            Assert.Equal(100, result);
        }

        [Fact]
        public void CreateAccessor_SimpleClass_Set_Value_100()
        {
            PropertyInfo pi = typeof(Assets.SimpleClass).GetProperty("Prop");
            PropertyAccessorsFactory factory = new PropertyAccessorsFactory();
            var instance = new Assets.SimpleClass(0);

            PropertyAccessor accessor = factory.CreateAccessor(pi);
            accessor.Set_Value(instance, 100);

            Assert.Equal(100, instance.Prop);
        }

        [Fact]
        public void CreateAccessor_GetOnyProp_Set_Value_Exception()
        {
            PropertyInfo pi = typeof(Assets.GetOnyProp).GetProperty("Prop");
            PropertyAccessorsFactory factory = new PropertyAccessorsFactory();
            var instance = new Assets.GetOnyProp(100);

            PropertyAccessor accessor = factory.CreateAccessor(pi);
            void result() => accessor.Set_Value(instance, 100);

            Assert.Throws<NotImplementedException>(() => result());
        }

        [Fact]
        public void CreateAccessor_SimpleObjectProp_Get_Value_SimpleObject_100()
        {
            PropertyInfo pi = typeof(Assets.SimpleObjectProp).GetProperty("Prop");
            PropertyAccessorsFactory factory = new PropertyAccessorsFactory();
            var instance = new Assets.SimpleObjectProp(100);

            PropertyAccessor accessor = factory.CreateAccessor(pi);
            object result = accessor.Get_Value(instance);

            Assert.Equal(instance.Prop, result);
        }

        [Fact]
        public void CreateAccessor_SimpleObjectProp_Set_Value_SimpleObject_100()
        {
            PropertyInfo pi = typeof(Assets.SimpleObjectProp).GetProperty("Prop");
            PropertyAccessorsFactory factory = new PropertyAccessorsFactory();
            var instance = new Assets.SimpleObjectProp(0);
            var arg = new Assets.SimpleClass(100);

            PropertyAccessor accessor = factory.CreateAccessor(pi);
            accessor.Set_Value(instance, arg);

            Assert.Equal(arg, instance.Prop);
        }

        [Fact]
        public void CreateAccessor_SimpleStructProp_Get_Value_SimpleObject_100()
        {
            PropertyInfo pi = typeof(Assets.SimpleStructProp).GetProperty("Prop");
            PropertyAccessorsFactory factory = new PropertyAccessorsFactory();
            var instance = new Assets.SimpleStructProp(100);

            PropertyAccessor accessor = factory.CreateAccessor(pi);
            object result = accessor.Get_Value(instance);

            Assert.Equal(instance.Prop, result);
        }

        [Fact]
        public void CreateAccessor_SimpleStructProp_Set_Value_SimpleObject_100()
        {
            PropertyInfo pi = typeof(Assets.SimpleStructProp).GetProperty("Prop");
            PropertyAccessorsFactory factory = new PropertyAccessorsFactory();
            var instance = new Assets.SimpleStructProp(0);
            var arg = new Assets.SimpleStruct(100);

            PropertyAccessor accessor = factory.CreateAccessor(pi);
            accessor.Set_Value(instance, arg);

            Assert.Equal(arg, instance.Prop);
        }

        [Fact]
        public void CreateAccessor_SimpleInterface_Get_Value_100() {
            PropertyInfo pi = typeof(Assets.SimpleInterface).GetProperty("Prop");
            PropertyAccessorsFactory factory = new PropertyAccessorsFactory();
            var instance = new Assets.SimpleInterfaceImp(100);

            PropertyAccessor accessor = factory.CreateAccessor(pi);
            object result = accessor.Get_Value(instance);

            Assert.Equal(instance.Prop, result);
        }

        [Fact]
        public void CreateAccessor_SimpleInterface_Set_Value_100() {
            PropertyInfo pi = typeof(Assets.SimpleInterface).GetProperty("Prop");
            PropertyAccessorsFactory factory = new PropertyAccessorsFactory();
            var instance = new Assets.SimpleInterfaceImp(0);

            PropertyAccessor accessor = factory.CreateAccessor(pi);
            accessor.Set_Value(instance, 100);

            Assert.Equal(100, instance.Prop);
        }
    }
}
