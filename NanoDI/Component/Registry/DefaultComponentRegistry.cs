using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NanoDI.Exceptions;
using NanoDI.Attributes;
using NanoDI.Component.Dependency;
using NanoDI.Container;
using NanoDI.Component.ComponentActivator;

namespace NanoDI.Component.Registry
{
    class DefaultComponentRegistry : IComponentRegistry
    {
        Dictionary<string, IComponent> components = new Dictionary<string, IComponent>();
        Dictionary<string, Type> componentTypes = new Dictionary<string, Type>();
        DependencyGraph dependencyGraph;

        public List<string> ComponentNames 
        {
            get{ return new List<String>(components.Keys); }
        }

        public void RegisterAll(List<IComponent> components)
        {
        	if (dependencyGraph==null)
        		dependencyGraph = new DependencyGraph(components.Count);
        	
        	foreach(IComponent component in components)
        	{
        		this.components.Add(component.Name, component);
        	}
        }

        // a little dirty, needs some refactoring and love
        object instantiateComponent(string componentName)
        {
            if (components.Keys.Contains(componentName))
                return components[componentName];

            Type actualComponentType = componentTypes[componentName];
            var actualComponentInstance = Activator.CreateInstance(actualComponentType);

            if (actualComponentType.AssemblyQualifiedName != null)
            {
                List<string> dependencies = getComponentDependencies(actualComponentType.AssemblyQualifiedName);
                
                if (dependencies.Count > 0)
                {
                    foreach (string dependency in dependencies)
                    {
                        string dependentComponentName = (from cmp in components where string.Equals(cmp.Value.Type.AssemblyQualifiedName, dependency) select cmp.Value.Name).FirstOrDefault();
                        FieldInfo fieldInfo = actualComponentType.GetField(dependentComponentName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                        if (fieldInfo != null)
                        {
                            object createdDependency = instantiateComponent(dependentComponentName);
                            fieldInfo.SetValue(actualComponentInstance, createdDependency);
                        }
                        else
                            throw new CompositionException();
                    }
                }
                // components.Add(componentName, actualComponentInstance);

                return actualComponentInstance;
            }
            else
                throw new InvalidComponentException(componentName);
        }

        public void addComponent(string name, IComponent component)
        {
            if (!components.ContainsKey(name))
                components.Add(name, component);
            else
                throw new ComponentAlreadyExistsException(name);
        }

        public List<object> getComponents()
        {
            return new List<object>();
        }

        void buildDependencyGraph()
        {
            foreach (Type componentType in componentTypes.Values)
            {
                foreach (FieldInfo fieldInfo in componentType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    addComponentDependencyIfInjectable(componentType, fieldInfo);
                }
            }
        }

        void addComponentDependencyIfInjectable(Type parentComponentType, FieldInfo fieldInfo)
        {
            foreach (Attribute attr in fieldInfo.GetCustomAttributes(true))
            {
                InjectAttribute inject = attr as InjectAttribute;
                if (inject != null)
                {
                    foreach (Type componentType in componentTypes.Values)
                    {
                        foreach (Type iFace in getImplementedInterfaces(componentType))
                        {
                            if (iFace.FullName.Equals(fieldInfo.FieldType.FullName))
                                dependencyGraph.insertDependency(parentComponentType.AssemblyQualifiedName, componentType.AssemblyQualifiedName);
                        }
                    }
                }
            }
        }

        public List<string> getComponentDependencies(string componentName)
        {
            return dependencyGraph.getDependencies(componentName);
        }

       
        private static Type[] getImplementedInterfaces(Type componentType)
        {
            return componentType.GetInterfaces();
        }


    	
		public List<IComponent> Components {
			get {
				throw new NotImplementedException();
			}
		}
    	
		public bool Contains(string componentName)
		{
			return components.ContainsKey(componentName);
		}
    	
		public IComponent Get(string componentName)
		{
			if(Contains(componentName))
				return components[componentName];
			else
				throw new InvalidComponentException(componentName);
		}
    	
		public Type GetType(string componentName)
		{
			return Get(componentName).GetType();
		}
    	
    }
}
