/** 
 * This File is part of the NDI Library
 * Copyright 2009,2010 Bogdan COSTEA <bogdan.costea@gridpulse.com>
 * 
 * This library is free software, published under the terms of the LGPL version 2.1 or newer.
 * More info in the LICENSE.TXT file in the root of the project.
 * 
 */

using System;

namespace Ndi.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentAttribute : Attribute
    {
        public ComponentAttribute(string name) { Name = name; Scope = Scope.Singleton; }

        public ComponentAttribute(string name, Scope scope) { Name = name; Scope = scope; }

        public string Name { get; private set; }

        public Scope Scope { get; private set; }
    }
}
