using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NanoDI.Exceptions;
using NanoDI.Attributes;

namespace NanoDI.Component.Cache
{
    class DefaultComponentCache:IComponentCache
    {
        Dictionary<IComponent, object> cache = new Dictionary<IComponent, object>();

        public bool Contains(IComponent component)
        {
            return cache.ContainsKey(component);
        }

        public object Get(IComponent component)
        {
            if (Contains(component))
                return cache[component];
            else
                throw new CompositionException();
        }

        public void Put(IComponent componentDefinition, object componentInstance)
        {
            if (isSingletonOverlap(componentDefinition) || isPrototype(componentDefinition))
            {
                throw new CompositionException();
            }
            else
            {
                cache.Add(componentDefinition, componentInstance);
            }
        }

        Boolean isSingletonOverlap(IComponent component)
        {
            return Contains(component) && Scope.Singleton.Equals(component.Scope);
        }

        Boolean isPrototype(IComponent component)
        {
            return Scope.Prototype.Equals(component.Scope);
        }

        public void Remove(IComponent component)
        {
            if (Contains(component))
            {
                cache.Remove(component);
            }
        }

    }
}
