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
using System.Text;
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
