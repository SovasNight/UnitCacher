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
        AssemblyBuilder assemblyBuilder;
        ModuleBuilder moduleBuilder;

        /// <summary>
        /// Initialize instance of <see cref="PropertyAccessorsFactory"/> and
        /// define dynamic assembly "PropertiesAccessors"
        /// </summary>
        public PropertyAccessorsFactory()
        {
            this.assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(
                new AssemblyName("PropertiesAccessors"), AssemblyBuilderAccess.RunAndCollect);

            this.moduleBuilder = this.assemblyBuilder.DefineDynamicModule("PropertiesAccessors.dll");
        }

        /// <summary>
        /// Create <see cref="PropertyAccessor"/> for specifed property
        /// using <see cref="PropertyInfo"/>
        /// </summary>
        public PropertyAccessor CreateAccessor(PropertyInfo pi)
        {
            Type baseType = typeof(PropertyAccessor);
            string typeName = string.Format("{0}_{1}_Accessor", pi.DeclaringType.Name, pi.Name);
            TypeBuilder typeBuilder = this.moduleBuilder.DefineType(typeName, TypeAttributes.Public, baseType);

            MethodInfo get_value = this.CreateGetter(typeBuilder, pi.GetGetMethod());
            typeBuilder.DefineMethodOverride(get_value, baseType.GetMethod("Get_Value"));

            MethodInfo set_value = this.CreateSetter(typeBuilder, pi.GetSetMethod());
            typeBuilder.DefineMethodOverride(set_value, baseType.GetMethod("Set_Value"));

            Type accessorType = typeBuilder.CreateType();
            
            return (PropertyAccessor)Activator.CreateInstance(accessorType);
        }

        private MethodInfo CreateGetter(TypeBuilder typeBuilder, MethodInfo mi)
        {
            Emit<Func<object, object>> emit = Emit<Func<object, object>>.BuildMethod(
                typeBuilder, "Get_Value",
                MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.Virtual,
                CallingConventions.HasThis);

            if (mi is null)
            {
                emit.NewObject<NotImplementedException>();
                emit.Throw();
            }
            else
            {
                emit.LoadArgument(1);
                emit.CastClass(mi.DeclaringType);

                emit.Call(mi);
                if (mi.ReturnType.IsValueType)
                    emit.Box(mi.ReturnType);
                else
                    emit.CastClass(mi.ReturnType);

                emit.Return();
            }
            return emit.CreateMethod();
        }

        private MethodInfo CreateSetter(TypeBuilder typeBuilder, MethodInfo mi)
        {
            Emit<Action<object, object>> emit = Emit<Action<object, object>>.BuildMethod(
                typeBuilder, "Set_Value", 
                MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.Virtual,
                CallingConventions.HasThis);

            if (mi is null)
            {
                emit.NewObject<NotImplementedException>();
                emit.Throw();
            }
            else
            {
                Type argType = mi.GetParameters()[0].ParameterType;

                emit.LoadArgument(1);
                emit.CastClass(mi.DeclaringType);

                emit.LoadArgument(2);
                if (argType.IsValueType)
                    emit.UnboxAny(argType);
                else
                    emit.CastClass(argType);

                emit.Call(mi);
                emit.Return();
            }

            return emit.CreateMethod();
        }
    }
}
