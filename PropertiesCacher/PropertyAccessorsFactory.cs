using System;
using System.Reflection;
using System.Reflection.Emit;

using Sigil;

namespace PropertiesCacher
{
    /// <summary>
    /// Provides methods to create <see cref="PropertyAccessor"/>'s to specifed properties.
    /// </summary>
    public class PropertyAccessorsFactory
    {
        private readonly AssemblyBuilder assemblyBuilder;
        private readonly ModuleBuilder moduleBuilder;

        /// <summary>
        /// Initialize instance of <see cref="PropertyAccessorsFactory"/> and
        /// define dynamic assembly "PropertiesAccessors"
        /// </summary>
        public PropertyAccessorsFactory() {
            this.assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(
                new AssemblyName("PropertiesAccessors"), AssemblyBuilderAccess.RunAndCollect);

            this.moduleBuilder = this.assemblyBuilder.DefineDynamicModule("PropertiesAccessors.dll");
        }

        /// <summary>
        /// Create <see cref="PropertyAccessor"/> for specifed property
        /// using <see cref="PropertyInfo"/>
        /// </summary>
        public PropertyAccessor CreateAccessor(PropertyInfo pi) {
            Type baseType = typeof(PropertyAccessor);
            string typeName = String.Format("{0}_{1}_Accessor", pi.DeclaringType.Name, pi.Name);
            TypeBuilder typeBuilder = this.moduleBuilder.DefineType(typeName, TypeAttributes.Public, baseType);

            this.DefineAccessorMethods(typeBuilder, pi);

            Type accessorType = typeBuilder.CreateType();

            return (PropertyAccessor)Activator.CreateInstance(accessorType);
        }

        /// <summary>
        /// Define and override methods:
        /// <see cref="PropertyAccessor.Get_Value(object)"/> and
        /// <see cref="PropertyAccessor.Set_Value(object, object)"/>
        /// </summary>
        /// <remarks>
        /// Use for creating descernats from <see cref="PropertyAccessor"/>
        /// </remarks>
        protected void DefineAccessorMethods(TypeBuilder typeBuilder, PropertyInfo pi) {
            MethodInfo get_value = this.CreateGetter(typeBuilder, pi.GetGetMethod());
            typeBuilder.DefineMethodOverride(get_value, typeBuilder.BaseType.GetMethod("Get_Value"));

            MethodInfo set_value = this.CreateSetter(typeBuilder, pi.GetSetMethod());
            typeBuilder.DefineMethodOverride(set_value, typeBuilder.BaseType.GetMethod("Set_Value"));
        }

        /// <summary>
        /// Create <see cref="PropertyFact"/> for specifed property
        /// using <see cref="PropertyInfo"/>
        /// </summary>
        public PropertyFact CreateFact(PropertyInfo pi) {
            Type baseType = typeof(PropertyFact);
            string typeName = String.Format("{0}_{1}_Fact", pi.DeclaringType.Name, pi.Name);
            TypeBuilder typeBuilder = this.moduleBuilder.DefineType(typeName, TypeAttributes.Public, baseType);

            ConstructorInfo baseCtor = baseType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)[0];
            _ = this.CreateConstructor(typeBuilder, baseCtor);

            this.DefineAccessorMethods(typeBuilder, pi);

            Type factType = typeBuilder.CreateType();

            ConstructorInfo ctor = factType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)[0];
            object[] args = new object[] { pi.Name, pi.PropertyType, pi.DeclaringType };
            return (PropertyFact)ctor.Invoke(args);
        }

        /// <summary>
        /// Create <see cref="PropertyFact"/> for all properties 
        /// wich passing <see cref="BindingFlags"/>
        /// </summary>
        public PropertyFact[] GetProperties(Type type, BindingFlags bindingAttr) {
            PropertyInfo[] properties = type.GetProperties(bindingAttr);
            var accessors = new PropertyFact[properties.Length];
            
            for (int i = 0; i < properties.Length; i++) {
                accessors[i] = this.CreateFact(properties[i]);
            }

            return accessors;
        }

        private MethodInfo CreateGetter(TypeBuilder typeBuilder, MethodInfo mi) {
            var emit = Emit<Func<object, object>>.BuildMethod(
                typeBuilder, "Get_Value",
                MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.Virtual,
                CallingConventions.HasThis);

            if (mi is null) {
                emit.NewObject<NotImplementedException>();
                emit.Throw();
            } else {
                if (!mi.IsStatic) {
                    emit.LoadArgument(1);
                    emit.CastClass(mi.DeclaringType);
                    emit.CallVirtual(mi);
                } else {
                    emit.Call(mi);
                }

                Type castType = mi.ReturnType;
                if (castType.IsByRef) {
                    castType = castType.GetElementType();
                    if (!castType.IsPrimitive && castType.IsValueType) {
                        emit.LoadObject(castType);
                    } else {
                        emit.LoadIndirect(castType);
                    }
                }

                if (castType.IsValueType){
                    emit.Box(castType);
                } else {
                    emit.CastClass(castType);
                }

                emit.Return();
            }
            return emit.CreateMethod();
        }

        private MethodInfo CreateSetter(TypeBuilder typeBuilder, MethodInfo mi) {
            var emit = Emit<Action<object, object>>.BuildMethod(
                typeBuilder, "Set_Value",
                MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.Virtual,
                CallingConventions.HasThis);

            if (mi is null) {
                emit.NewObject<NotImplementedException>();
                emit.Throw();
            } else {
                Type argType = mi.GetParameters()[0].ParameterType;

                if (!mi.IsStatic) {
                    emit.LoadArgument(1);
                    emit.CastClass(mi.DeclaringType);
                }

                emit.LoadArgument(2);
                if (argType.IsValueType) {
                    emit.UnboxAny(argType);
                } else {
                    emit.CastClass(argType);
                }

                if (!mi.IsStatic) {
                    emit.CallVirtual(mi);
                } else {
                    emit.Call(mi);
                }
                emit.Return();
            }

            return emit.CreateMethod();
        }

        private ConstructorInfo CreateConstructor(TypeBuilder typeBuilder, ConstructorInfo baseConstructor) {
            var emit = Emit<Action<string, Type, Type>>.BuildConstructor(
                typeBuilder, MethodAttributes.Private);

            emit.LoadArgument(0);
            emit.LoadArgument(1);
            emit.LoadArgument(2);
            emit.LoadArgument(3);

            emit.Call(baseConstructor);
            emit.Return();

            return emit.CreateConstructor();
        }
    }
}
