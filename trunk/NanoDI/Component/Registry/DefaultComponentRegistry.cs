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
        //Dictionary<string, Type> componentTypes = new Dictionary<string, Type>();
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
            
            buildDependencyGraph();
        }


        public void addComponent(string name, IComponent component)
        {
            if (!components.ContainsKey(name))
                components.Add(name, component);
            else
                throw new ComponentAlreadyExistsException(name);
        }

        void buildDependencyGraph()
        {
            foreach (IComponent component in components.Values)
            {
                foreach (FieldInfo fieldInfo in component.Type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    addComponentDependencyIfInjectable(component, fieldInfo);
                }
            }
        }

        void addComponentDependencyIfInjectable(IComponent parentComponent, FieldInfo fieldInfo)
        {
            foreach (Attribute attr in fieldInfo.GetCustomAttributes(true))
            {
                InjectAttribute inject = attr as InjectAttribute;
                if (inject != null)
                {
                    foreach (IComponent component in components.Values)
                    {
                        foreach (Type iFace in getImplementedInterfaces(component.Type))
                        {
                            if (iFace.FullName.Equals(fieldInfo.FieldType.FullName))
                                dependencyGraph.insertDependency(parentComponent.Name, component.Name);
                        }
                    }
                }
            }
        }

        public void Put(IComponent component)
        {
            if (!Contains(component.Name))
            {
                components.Add(component.Name, component);
            }
            else if (Contains(component.Name) && Scope.Singleton.Equals(component.Scope))
            {
                throw new CompositionException();
            }


        }

        public List<IComponent> getComponentDependencies(IComponent component)
        {
            List<IComponent> dependencies = new List<IComponent>();
            List<string> stringDependencies = dependencyGraph.getDependencies(component.Name);

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
