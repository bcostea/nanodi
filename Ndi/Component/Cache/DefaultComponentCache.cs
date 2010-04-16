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
using Ndi.Exceptions;
using Ndi.Attributes;

namespace Ndi.Component.Cache
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
			if (Contains(componentDefinition) || isPrototype(componentDefinition))
            {
                throw new CompositionException();
            }
            else
            {
                cache.Add(componentDefinition, componentInstance);
            }
        }

        static Boolean isPrototype(IComponent component)
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

        public void Clear()
        {
            cache.Clear();
        }

    }
}
