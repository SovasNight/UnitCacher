using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Xunit;

#pragma warning disable IDE0008

namespace PropertiesCacher.Tests
{
    public class PropertyAccessorsFactoryTests
    {
        public static IEnumerable<object[]> TestClass_All_PropertyName() {
            return typeof(Assets.TestClass).GetProperties().Select(
                p => new object[] { p.Name }).ToList();
        }

        [Theory]
        [MemberData(nameof(TestClass_All_PropertyName))]
        public void CreateAccesor_TestClass_Get_Value(string pName) {
            PropertyInfo pi = typeof(Assets.TestClass).GetProperty(pName);
            var instance = new Assets.TestClass(100);
            var except = pi.GetValue(instance);

            PropertyAccessor accessor = new PropertyAccessorsFactory().CreateAccessor(pi);
            object result = accessor.Get_Value(instance);

            Assert.Equal(except, result);
        }

        public static IEnumerable<object[]> TestClass_CanWrite_PropertyName() {
            return typeof(Assets.TestClass).GetProperties().Where(p => p.CanWrite).Select(
                p => new object[] { p.Name }).ToList();
        }

        [Theory]
        [MemberData(nameof(TestClass_CanWrite_PropertyName))]
        public void CreateAccesor_TestClass_Set_Value(string pName) {
            PropertyInfo pi = typeof(Assets.TestClass).GetProperty(pName);
            var instance = new Assets.TestClass(100);
            var except = pi.GetValue(instance);

            PropertyAccessor accessor = new PropertyAccessorsFactory().CreateAccessor(pi);
            accessor.Set_Value(instance, except);

            object result = pi.GetValue(instance);
            Assert.Equal(except, result);
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
        public void CreateAccessor_SimpleInterface_Get_Value_100() {
            PropertyInfo pi = typeof(Assets.SimpleInterface).GetProperty("Prop");
            var instance = new Assets.TestInterfaceImp(100);
            var except = 100;

            PropertyAccessor accessor = new PropertyAccessorsFactory().CreateAccessor(pi);
            object result = accessor.Get_Value(instance);

            Assert.Equal(except, result);
        }

        [Fact]
        public void CreateAccessor_SimpleInterface_Set_Value_100() {
            PropertyInfo pi = typeof(Assets.SimpleInterface).GetProperty("Prop");
            var instance = new Assets.TestInterfaceImp(100);
            var except = 100;

            PropertyAccessor accessor = new PropertyAccessorsFactory().CreateAccessor(pi);
            accessor.Set_Value(instance, except);

            object result = instance.Prop;
            Assert.Equal(except, result);
        }

        public static IEnumerable<object[]> TestRefProperyClass_All_PropertyName() {
            return typeof(Assets.TestRefPropClass).GetProperties().Select(
                p => new object[] { p.Name }).ToList();
        }

        [Theory]
        [MemberData(nameof(TestRefProperyClass_All_PropertyName))]
        public void CreateAccesor_TestRefProperyClass_Get_Value(string pName) {
            PropertyInfo pi = typeof(Assets.TestRefPropClass).GetProperty(pName);
            var instance = new Assets.TestRefPropClass(100);
            var except = pi.GetValue(instance);

            PropertyAccessor accessor = new PropertyAccessorsFactory().CreateAccessor(pi);
            object result = accessor.Get_Value(instance);

            Assert.Equal(except, result);
        }

        [Fact]
        public void CreateAccesor_TestStaticPropClass_Get_Value_100() {
            PropertyInfo pi = typeof(Assets.TestStaticPropClass).GetProperty("Prop");
            Assets.TestStaticPropClass.Prop = 100;
            var except = 100;

            PropertyAccessor accessor = new PropertyAccessorsFactory().CreateAccessor(pi);
            object result = accessor.Get_Value(null);

            Assert.Equal(except, result);
        }

        [Fact]
        public void CreateAccesor_TestStaticPropClass_Set_Value_100() {
            PropertyInfo pi = typeof(Assets.TestStaticPropClass).GetProperty("Prop");
            var except = 100;

            PropertyAccessor accessor = new PropertyAccessorsFactory().CreateAccessor(pi);
            accessor.Set_Value(null, except);

            object result = Assets.TestStaticPropClass.Prop;
            Assert.Equal(except, result);
        }

        [Fact]
        public void CreateAccesor_SimpleStaticClass_Get_Value() {
            PropertyInfo pi = typeof(Assets.SimpleStaticClass).GetProperty("Prop");
            Assets.SimpleStaticClass.Prop = 100;
            var except = 100;

            PropertyAccessor accessor = new PropertyAccessorsFactory().CreateAccessor(pi);
            object result = accessor.Get_Value(null);

            Assert.Equal(except, result);
        }

        [Fact]
        public void CreateAccesor_SimpleStaticClass_Set_Value() {
            PropertyInfo pi = typeof(Assets.SimpleStaticClass).GetProperty("Prop");
            var except = 100;

            PropertyAccessor accessor = new PropertyAccessorsFactory().CreateAccessor(pi);
            accessor.Set_Value(null, except);

            object result = Assets.SimpleStaticClass.Prop;
            Assert.Equal(except, result);
        }

        [Fact]
        public void CreateFact_TestClass_PropsEqualsFact() {
            PropertyInfo pi = typeof(Assets.TestClass).GetProperty("IntProp");

            PropertyFact fact = new PropertyAccessorsFactory().CreateFact(pi);

            Assert.True(
                fact.PropertyName == pi.Name &&
                fact.PropertyType == pi.PropertyType &&
                fact.DeclaringType == pi.DeclaringType
                );
        }
    }
}
