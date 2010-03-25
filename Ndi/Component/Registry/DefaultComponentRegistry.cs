/** 
 * This File is part of the NDI Library
 * Copyright 2009,2010 Bogdan COSTEA <bogdan.costea@gridpulse.com>
 * 
 * This library is free software, published under the terms of the LGPL version 2.1 or newer.
 * More info in the LICENSE.TXT file in the root of the project.
 * 
 */

using System;
using System.Collections.Generic;
using System.Reflection;
using Ndi.Exceptions;
using Ndi.Component.Dependency;
using Ndi.Tooling.Logging;

namespace Ndi.Component.Registry
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
				validateComponent(component);
                extractAndAddDependencies(component);
            }
        }

        void extractAndAddDependencies(IComponent component)
        {
			foreach (ComponentField componentField in component.Fields)
			{
				FieldInfo possibleDependencyField = getDependencyField(component, componentField.Name);
				
                addComponentDependencyIfValid(component, possibleDependencyField);
			}
        }

        private FieldInfo getDependencyField(IComponent component, string fieldName)
        {
            FieldInfo field = component.Type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (field != null)
                return field;
            else
                throw new CompositionException("Component '" + component.Name + "' does not contain field '" + fieldName + "'");
        }

        void validateComponent(IComponent component)
		{
			if (component != null)
			{
				if (component.Name == null || component.Name.Length < 1 || component.Type == null)
					throw new CompositionException();
			}
		}

        void addComponentDependencyIfValid(IComponent parentComponent, FieldInfo fieldInfo)
        {
            if (fieldIsValidComponent(fieldInfo))
            {
                IComponent dependency = registeredComponents[fieldInfo.Name];

                if (log.IsDebugEnabled())
                    log.Debug("Added '" + dependency.Name + "' as a dependency of '" + parentComponent.Name + "'");

                dependencyGraph.insertDependency(parentComponent.Name, dependency.Name);
            }
        }

       

        Boolean fieldIsValidComponent(FieldInfo fieldInfo)
        {
            if(log.IsDebugEnabled())
                log.Debug( "Validating field " + fieldInfo.Name);
            
            return registeredComponents.ContainsKey(fieldInfo.Name)
                && (componentHasInterface(registeredComponents[fieldInfo.Name], fieldInfo.FieldType) || 
                    componentHasSameType(registeredComponents[fieldInfo.Name], fieldInfo.FieldType));
        }

        private bool componentHasSameType(IComponent iComponent, Type type)
        {
            return type.Equals(iComponent.Type);    
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

            foreach (string dependencyName in stringDependencies)
            {
                if (ContainsComponent(dependencyName))
                {
                    dependencies.Add(GetComponent(dependencyName));
                }
            }

            return dependencies;
        }


        private static Type[] getImplementedInterfaces(Type componentType)
        {
            return componentType.GetInterfaces();
        }

        public bool ContainsComponent(string componentName)
        {
            return registeredComponents.ContainsKey(componentName);
        }

        public IComponent GetComponent(string componentName)
        {
            if (ContainsComponent(componentName))
                return registeredComponents[componentName];
            else
                throw new InvalidComponentException(componentName);
        }
    }
}
