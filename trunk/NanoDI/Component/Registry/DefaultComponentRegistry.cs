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
using System.Linq;
using System.Reflection;
using NanoDI.Exceptions;
using NanoDI.Attributes;
using NanoDI.Component.Dependency;
using NanoDI.Container;
using NanoDI.Component.ComponentActivator;
using NanoDI.Tooling.Logging;

namespace NanoDI.Component.Registry
{
    class DefaultComponentRegistry : IComponentRegistry
    {
        ILogger log = LogFactory.GetLog(typeof(DefaultComponentRegistry));

        Dictionary<string, IComponent> registeredComponents = new Dictionary<string, IComponent>();
        DependencyGraph dependencyGraph;

        /// <summary>
        /// Registers a list of components and their dependencies
        /// </summary>
        /// <param name="components">A list of components</param>
        public void RegisterAll(List<IComponent> components)
        {
            initializeDependencyGraph(components);
            registerComponents(components);
            registerDependenciesForComponents(components);
        }

        /// <summary>
        /// Registers a component and it's dependencies
        /// </summary>
        /// <param name="component">A component</param>
        public void RegisterComponent(IComponent component)
        {
            registerComponent(component.Name, component);
            extractAndAddDependencies(component);
        }

        public void UnregisterAll()
        {
            if (log.IsDebugEnabled())
                log.Debug("Unregistering components.");

            registeredComponents.Clear();
            dependencyGraph.Clear();
        }

        void initializeDependencyGraph(List<IComponent> components)
        {
            if (dependencyGraph == null)
                dependencyGraph = new DependencyGraph(components.Count);
        }

        void registerComponents(List<IComponent> components)
        {
            foreach (IComponent component in components)
            {
                registerComponent(component.Name, component);
            }
        }

        void registerComponent(string name, IComponent component)
        {
            if (log.IsDebugEnabled())
                log.Debug("Trying to register component '" + name + "'");

            if (!registeredComponents.ContainsKey(name))
                registeredComponents.Add(name, component);
            else
                throw new ComponentAlreadyExistsException(name);
        }

        void registerDependenciesForComponents(List<IComponent> components)
        {
            foreach (IComponent component in components)
            {
                extractAndAddDependencies(component);
            }
        }

        void extractAndAddDependencies(IComponent component)
        {
            foreach (FieldInfo possibleDependencyField in component.Type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (log.IsDebugEnabled())
                    log.Debug("Validating field '" + possibleDependencyField.Name + "' of component '" + component.Name + "'");

                addComponentDependencyIfInjectable(component, possibleDependencyField);
            }
        }

        void addComponentDependencyIfInjectable(IComponent parentComponent, FieldInfo fieldInfo)
        {
            if (fieldIsInjectable(fieldInfo) && fieldIsValidComponent(fieldInfo))
            {
                IComponent dependency = registeredComponents[fieldInfo.Name];

                if (log.IsDebugEnabled())
                    log.Debug("Added '" + dependency.Name + "' as a dependency of '" + parentComponent.Name + "'");

                dependencyGraph.insertDependency(parentComponent.Name, dependency.Name);
            }
        }

        Boolean fieldIsInjectable(FieldInfo fieldInfo)
        {
            foreach (Attribute attr in fieldInfo.GetCustomAttributes(true))
            {
                InjectAttribute inject = attr as InjectAttribute;
                if (inject != null)
                {
                    return true;
                }
            }
            return false;
        }

        Boolean fieldIsValidComponent(FieldInfo fieldInfo)
        {
            if(log.IsDebugEnabled())
                log.Debug( fieldInfo.Name + " is valid component.");
            
            return registeredComponents.ContainsKey(fieldInfo.Name)
                && componentHasInterface(registeredComponents[fieldInfo.Name], fieldInfo.FieldType);
        }


        Boolean componentHasInterface(IComponent component, Type theInterface)
        {
            
            foreach (Type iFace in getImplementedInterfaces(component.Type))
            {
                if (iFace.FullName.Equals(theInterface.FullName))
                {
                    if (log.IsDebugEnabled())
                        log.Debug(component.Name + " implements correct interface.");
            
                    return true;
                }
            }
            return false;
        }

        public List<IComponent> GetComponentDependencies(IComponent component)
        {
            List<IComponent> dependencies = new List<IComponent>();
            List<string> stringDependencies = dependencyGraph.GetDependencies(component.Name);

            foreach (string dep in stringDependencies)
            {
                if (Contains(dep))
                {
                    dependencies.Add(Get(dep));
                }
                else
                    throw new CompositionException();
            }

            return dependencies;
        }


        private static Type[] getImplementedInterfaces(Type componentType)
        {
            return componentType.GetInterfaces();
        }

        public bool Contains(string componentName)
        {
            return registeredComponents.ContainsKey(componentName);
        }

        public IComponent Get(string componentName)
        {
            if (Contains(componentName))
                return registeredComponents[componentName];
            else
                throw new InvalidComponentException(componentName);
        }

        public Type GetType(string componentName)
        {
            return Get(componentName).GetType();
        }

    }
}
