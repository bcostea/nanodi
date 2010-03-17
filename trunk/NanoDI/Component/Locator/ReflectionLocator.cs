#region Copyright 2009 Bogdan COSTEA
/** This File is part of the NanoDI Library
 *
 * Copyright 2009 Bogdan COSTEA
 * All rights reserved
 * 
 * This library is free software; you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published
 * by the Free Software Foundation; either version 2.1 of the License, or
 * (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful, but
 * WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
 * or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with this library; if not, write to the
 * Free Software Foundation, Inc.,
 * 51 Franklin Street, Fifth Floor Boston, MA  02110-1301 USA
 */
#endregion

using System;
using System.Collections.Generic;
using Ndi.Attributes;
using System.Reflection;
using System.Linq;
using Ndi.Tooling.Logging;
using Ndi.Exceptions;

namespace Ndi.Component.Locator
{
    class ReflectionLocator : ILocator
    {
        ILogger log = LogFactory.GetLog(typeof(ReflectionLocator));

        public List<IComponent> Locate()
        {
            return Locate("");
        }

        public List<IComponent> Locate(string targetNamespace)
        {
            List<Type> types = new List<Type>();

            if (log.IsDebugEnabled())
            {
                log.Debug("Locating components defined in namespace \"" + targetNamespace + "\"");
            }

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly asm in assemblies)
            {
                IEnumerable<Type> asmTypes = from type in asm.GetTypes()
                                             where type.IsClass &&
                                                 (type.Namespace != null && type.Namespace.StartsWith(targetNamespace))
                                             select type;
                types.AddRange(asmTypes);
            }

            return LocateInTypes(types.ToArray());
        }

        public List<IComponent> LocateInTypes(Type[] types)
        {

            IEnumerable<Component> components =
                from type in types
                where type.GetCustomAttributes(typeof(ComponentAttribute), true).Length > 0
                from attribute in type.GetCustomAttributes(typeof(ComponentAttribute), true)
                select new Component(((ComponentAttribute)attribute).Name,
                                     type,
                                     ((ComponentAttribute)attribute).Scope,
                                     getInjectables(type));

            return components.ToList().ConvertAll(new Converter<Component, IComponent>(component => component));

        }

        List<ComponentField> getInjectables(Type type)
        {
            List<ComponentField> componentFields = new List<ComponentField>();

            componentFields.AddRange(getInjectableFields(type));
            componentFields.AddRange(getInjectableConstructorFields(type));

            return componentFields;
        }

        List<ComponentField> getInjectableFields(Type type)
        {
            IEnumerable<ComponentField> fields =
                from possibleDependencyField in type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                where fieldIsInjectable(possibleDependencyField)
                select getComponentField(possibleDependencyField);

            return fields.ToList();
        }

        ComponentField getComponentField(MemberInfo possibleField)
        {
            return new ComponentField(possibleField.Name, getInjectMethod(possibleField));

        }

        List<ComponentField> getInjectableConstructorFields(Type type)
        {
            IEnumerable<ComponentField> fields = new List<ComponentField>();

            ConstructorInfo constructorInfo = getInjectableConstructor(type);

            if (constructorInfo != null)
            {
                fields = from parameterInfor in constructorInfo.GetParameters()
                         select new ComponentField(parameterInfor.Name, InjectMethod.Constructor);
            }

            return fields.ToList();
        }

        private bool constructorIsInjectable(ConstructorInfo possibleInjectableConstructor)
        {
            return !getInjectMethod(possibleInjectableConstructor).Equals(InjectMethod.None);
        }

        private InjectMethod getInjectMethod(MemberInfo memberInfo)
        {
            foreach (Attribute attr in memberInfo.GetCustomAttributes(true))
            {
                InjectAttribute inject = attr as InjectAttribute;
                if (inject != null)
                {
                    // if an inject method was clearly specified
                    if (!InjectMethod.None.Equals(inject.InjectMethod))
                    {
                        return inject.InjectMethod;
                    }
                    else
                    {

                        if (MemberTypes.Property.Equals(memberInfo.MemberType))
                        {
                            return InjectMethod.Setter;
                        }
                        else if (MemberTypes.Constructor.Equals(memberInfo.MemberType))
                        {
                            return InjectMethod.Constructor;
                        }
                        else
                        {
                            return InjectMethod.Field;
                        }
                    }
                }
            }

            // never inject it
            return InjectMethod.None;
        }

        private bool fieldIsInjectable(FieldInfo fieldInfo)
        {
            return !getInjectMethod(fieldInfo).Equals(InjectMethod.None);
        }

        public ConstructorInfo getInjectableConstructor(Type type)
        {
            IEnumerable<ConstructorInfo> ctrs =
                from possibleInjectableConstructor in type.GetConstructors()
                where constructorIsInjectable(possibleInjectableConstructor)
                select possibleInjectableConstructor;

            if (ctrs.Count() < 1)
                return null;
            if (ctrs.Count() > 1)
                throw new CompositionException("Only one constructor can be configured for injection using reflection");

            return ctrs.ToList()[0];
        }

    }
}
