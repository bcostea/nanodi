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
using Ndi.Attributes;
using Ndi.Component.Registry;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Ndi.Exceptions;
using Ndi.Component.Cache;

namespace Ndi.Component.ComponentActivator
{
    class DefaultComponentActivator : IComponentActivator
    {
        private IComponentRegistry componentRegistry;
        private IComponentCache componentCache;

        public DefaultComponentActivator(IComponentRegistry componentRegistry, IComponentCache componentCache)
        {
            this.componentRegistry = componentRegistry;
            this.componentCache = componentCache;
        }

        public object GetInstance(IComponent component)
        {
            if (Scope.Singleton.Equals(component.Scope) && componentCache.Contains(component))
                return componentCache.Get(component);

            return createInstance(component);

        }
      
        private void instantiateAndApplyFields(IComponent component, object componentInstance)
        {
            List<IComponent> dependencies = componentRegistry.GetComponentDependencies(component);

            if (dependencies.Count > 0)
            {
                foreach (IComponent dependentComponent in dependencies)
                {
                    // constructor parameters have already been applied
                    if (!isConstructorParameter(component, dependentComponent))
                    {
                        FieldInfo fieldInfo = component.Type.GetField(dependentComponent.Name, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                        if (fieldInfo != null)
                        {
                            object createdDependency = GetInstance(dependentComponent);
                            fieldInfo.SetValue(componentInstance, createdDependency);
                        }
                    }
                }
            }
        }

        private bool isConstructorParameter(IComponent component, IComponent dependentComponent)
        {
            ConstructorInfo ctrInfo = getMatchingConstructor(component);
            IEnumerable<string> parameterNames = from param in ctrInfo.GetParameters()
                                                 select param.Name;
            return parameterNames.ToList().Contains(dependentComponent.Name);
        }


        object createInstance(IComponent component)
        {
            object componentInstance = null;

            if (requiresConstructorInject(component))
            {
                ConstructorInfo ctrInfo = getMatchingConstructor(component);
                object[] parameters = getInstantiatedConstructorParameters(ctrInfo);
                componentInstance = ctrInfo.Invoke(parameters);
            }
            else
            {
                componentInstance = Activator.CreateInstance(component.Type);
            }

            instantiateAndApplyFields(component, componentInstance);
            addToCache(component, componentInstance);
            return componentInstance;
        }

        private void addToCache(IComponent component, object componentInstance)
        {
            if (Scope.Singleton.Equals(component.Scope))
            {
                componentCache.Put(component, componentInstance);
            }
        }

        private object[] getInstantiatedConstructorParameters(ConstructorInfo ctrInfo)
        {

            List<object> parameters = new List<object>();
            foreach (ParameterInfo paramInfo in ctrInfo.GetParameters())
            {
                parameters.Add(GetInstance(componentRegistry.GetComponent(paramInfo.Name)));
            }
            return parameters.ToArray();
        }

        private ConstructorInfo getMatchingConstructor(IComponent component)
        {
            ConstructorInfo matchingConstructor=null;

            IEnumerable<string> injectionParameterNames = from componentField in component.Fields
                                                          where InjectMethod.Constructor.Equals(componentField.InjectMethod)
                                                          select componentField.Name;

            foreach (ConstructorInfo ctrInfo in component.Type.GetConstructors())
            {
                IEnumerable<string> parameterNames = from param in ctrInfo.GetParameters()
                                                     where !injectionParameterNames.ToList().Contains(param.Name)
                                                     select param.Name;
                if (parameterNames.Count() > 0)
                    continue;
                else
                    matchingConstructor = ctrInfo;
            }

            return matchingConstructor;
        }

        bool requiresConstructorInject(IComponent component)
        {
            foreach (ComponentField field in component.Fields)
            {
                if (InjectMethod.Constructor.Equals(field.InjectMethod))
                    return true;
            }
            return false;
        }

    }


}
