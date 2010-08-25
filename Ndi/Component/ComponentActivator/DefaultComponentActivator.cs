/** 
 * This File is part of the NDI Library
 * Copyright 2009,2010 Bogdan COSTEA <bogdan.costea@gridpulse.com>
 * 
 * This library is free software, published under the terms of the LGPL version 2.1 or newer.
 * More info in the LICENSE.TXT file in the root of the project.
 * 
 */

using System;
using Ndi.Attributes;
using Ndi.Component.Registry;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Ndi.Component.Cache;
using Ndi.Exceptions;

namespace Ndi.Component.ComponentActivator
{
    /// <summary>
    /// The instance creator.
    /// Requires a component registry and a component cache
    /// </summary>
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

            try
            {
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
            } catch(TargetInvocationException tie)
            {
                throw new CompositionException("Cannot instantiate component. Check minimal constructor for field or parameter dependencies.", tie);
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
