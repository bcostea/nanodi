using System;
using System.Collections.Generic;
using NanoDI.Attributes;
using System.Reflection;

namespace NanoDI.Component.Locator
{
    class ReflectionLocator : ILocator
    {
        Assembly asm = Assembly.GetEntryAssembly();

        public List<IComponent> Locate()
        {
            return LocateInNamespace(asm.EntryPoint.DeclaringType.Namespace);
        }
        
        public List<IComponent> LocateInNamespace(string namespaceName)
        {
            List<IComponent> components = new List<IComponent>();
            foreach (Type type in asm.GetTypes())
            {
                foreach (Attribute attr in type.GetCustomAttributes(true))
                {
                    ComponentAttribute component = attr as ComponentAttribute;
                    if (component != null)
                    {
                        components.Add(new Component(component.Name, type, component.Scope));
                    }
                }
            }
            return components;
        }
    }
}
