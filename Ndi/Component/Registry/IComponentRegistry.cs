/** 
 * This File is part of the NDI Library
 * Copyright 2009-2016 Bogdan COSTEA <bogdan@costea.us>
 * 
 * This library is free software, published under the terms of the LGPL version 2.1 or newer.
 * More info in the LICENSE.TXT file in the root of the project.
 * 
 */

using System;
using System.Collections.Generic;

namespace Ndi.Component.Registry
{
    interface IComponentRegistry
    {
        
        Boolean ContainsComponent(string componentName);
        /**
         * Get a configured component by name
         **/
        IComponent GetComponent(string componentName);

        /**
         * Get a configured component by name, use type as a fallback.
         * If there's only one component of a specific type, provide that one, regardless of the name.
         **/
        IComponent GetComponent(string componentName, Type componentType);

        void RegisterAll(List<IComponent> components);

        List<IComponent> GetComponentDependencies(IComponent component);

        void UnregisterAll();
    }
}
