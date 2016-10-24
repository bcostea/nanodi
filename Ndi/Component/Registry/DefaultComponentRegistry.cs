/** 
 * This File is part of the NDI Library
 * Copyright 2009-2016 Bogdan COSTEA <bogdan@costea.us>
 * 
 * This library is free software, published under the terms of the LGPL version 2.1 or newer.
 * More info in the LICENSE.TXT file in the root of the project.
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Ndi.Attributes;
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

        /// <summary>
        /// Unregisters all components and clears the dependency graph
        /// </summary>
        public void UnregisterAll()
        {
            if (log.IsDebugEnabled())
                log.Debug("Unregistering components.");

            registeredComponents.Clear();
            dependencyGraph.Clear();
        }

        /// <summary>
        /// Initialized the dependency graph for a list of components
        /// </summary>
        /// <param name="components"></param>
        void initializeDependencyGraph(List<IComponent> components)
        {
            if (dependencyGraph == null)
                dependencyGraph = new DependencyGraph(components.Count);
        }

        /// <summary>
        /// Registeres a list of components in the registry
        /// </summary>
        /// <param name="components">the list of components to be registered</param>
        void registerComponents(List<IComponent> components)
        {
            foreach (IComponent component in components)
            {
                registerComponent(component.Name, component);
            }
        }
        /// <summary>
        /// Registers a component
        /// </summary>
        /// <param name="name">the component name</param>
        /// <param name="component">the component definition</param>
        void registerComponent(string name, IComponent component)
        {
            if (log.IsDebugEnabled())
                log.Debug("Trying to register component '" + name + "'");

            if (!registeredComponents.ContainsKey(name))
            {
                validateComponent(component);
                registeredComponents.Add(name, component);
            }
            else
                throw new ComponentAlreadyExistsException(name);
        }

        /// <summary>
        /// registers all component dependencies
        /// </summary>
        /// <param name="components">the list components</param>
        void registerDependenciesForComponents(List<IComponent> components)
        {
            foreach (IComponent component in components)
            {
				validateComponent(component);
                extractAndAddDependencies(component);
            }
        }

        /// <summary>
        /// Extracts the component dependencies based on component fields and adds them to the registry
        /// </summary>
        /// <param name="component"></param>
        void extractAndAddDependencies(IComponent component)
        {
			foreach (ComponentField componentField in component.Fields)
			{
                // Constructor injection doesn't require an existing field so we just skip this
			    if (componentField.InjectMethod != InjectMethod.Constructor)
			    {
			        FieldInfo possibleDependencyField = getDependencyField(component, componentField.Name);
			        addComponentDependencyIfValid(component, possibleDependencyField);
			    }
			}
        }

        /// <summary>
        /// Retrieves a component field based on field name
        /// </summary>
        /// <param name="component">the component that contains the field</param>
        /// <param name="fieldName">the field name</param>
        /// <returns></returns>
        private FieldInfo getDependencyField(IComponent component, string fieldName)
        {
            FieldInfo field = component.Type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (field != null)
                return field;
            else
                throw new CompositionException("Component '" + component.Name + "' does not contain field '" + fieldName + "'");
        }

        /// <summary>
        /// Validates a component.
        /// Checks for component name and component Type.
        /// </summary>
        /// <param name="component">the component definition</param>
        void validateComponent(IComponent component)
		{
			if (component != null)
			{
				if (component.Name == null || component.Name.Length < 1)
					throw new CompositionException( "Invalid component" );

                if(component.Type == null)
                    throw  new CompositionException( "Invalid type for component '" + component.Name + "'");
			}
		}

        /// <summary>
        /// Adds a component dependency to the registry also checking for validity.
        /// The component is set as a dependency in the dependency graph.
        /// </summary>
        /// <param name="parentComponent"></param>
        /// <param name="fieldInfo"></param>
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

       
        /// <summary>
        /// Checks if a component field is a valid component.
        /// This is required to ensure that all fields that are dependencies match the correct definition
        /// </summary>
        /// <param name="fieldInfo"></param>
        /// <returns></returns>
        Boolean fieldIsValidComponent(FieldInfo fieldInfo)
        {
            if(log.IsDebugEnabled())
                log.Debug( "Validating field " + fieldInfo.Name);
            
            return registeredComponents.ContainsKey(fieldInfo.Name)
                && (componentHasInterface(registeredComponents[fieldInfo.Name], fieldInfo.FieldType) || 
                    componentHasSameType(registeredComponents[fieldInfo.Name], fieldInfo.FieldType));
        }

        /// <summary>
        /// Checks if a component dependency has the correct type
        /// </summary>
        /// <param name="iComponent">the component definition</param>
        /// <param name="type">the dependency type</param>
        /// <returns></returns>
        private bool componentHasSameType(IComponent iComponent, Type type)
        {
            return type.Equals(iComponent.Type);    
        }

        /// <summary>
        /// Checks if a component implements the correct interface, to ensure correct injection
        /// </summary>
        /// <param name="component">the component definition</param>
        /// <param name="theInterface">the interface type of the dependency</param>
        /// <returns>true if it implements the correct interface</returns>
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

        public bool ContainsType(Type componentType)
        {
            return GetComponentsByType(componentType).Count > 0;
        }

        public List<IComponent> GetComponentsByType(Type componentType)
        {
            List<IComponent> components = new List<IComponent>();

            foreach (IComponent component in registeredComponents.Values)
            {
                if (component.Type == componentType)
                {
                    components.Add(component);
                }
            }
            return components;
        }

        /// <summary>
        /// Retrieves the component definition from the registry 
        /// </summary>
        /// <param name="componentName">the component name</param>
        /// <returns>the component definition</returns>
        public IComponent GetComponent(string componentName)
        {
            if (ContainsComponent(componentName))
                return registeredComponents[componentName];
            else
                throw new InvalidComponentException(componentName);
        }

        public IComponent GetComponent(string componentName, Type componentType)
        {
            if (ContainsComponent(componentName))
                return registeredComponents[componentName];

            if (ContainsType(componentType))
            {
                List<IComponent> components = GetComponentsByType(componentType);
                if (components.Count == 1)
                {
                    return GetComponentsByType(componentType)[0];
                }
            }
            throw new InvalidComponentException(componentName);

        }
    }
}
