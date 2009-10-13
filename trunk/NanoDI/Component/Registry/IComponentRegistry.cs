using System;
using System.Collections.Generic;

namespace NanoDI.Component.Registry
{
    interface IComponentRegistry
    {
        List<string> ComponentNames{ get; }
        List<IComponent> Components { get; }

        Boolean Contains(string componentName);
        IComponent Get(string componentName);
        Type GetType(string componentName);
        void Put(IComponent component);
        
        void RegisterAll(List<IComponent> components);

        List<IComponent> getComponentDependencies(IComponent component);
    }
}
