/** 
 * This File is part of the NDI Library Samples
 * Copyright 2009,2010 Bogdan COSTEA <bogdan.costea@gridpulse.com>
 * 
 * This library is free software, published under the terms of the LGPL version 2.1 or newer.
 * More info in the LICENSE.TXT file in the root of the project.
 * 
 */

using System;

using Ndi.Attributes;

namespace NdiExample.Dependencies
{
    [Component("dependencyOne")]
    class DependencyOne:IDependency
    {
        public void Start()
        {
            Console.WriteLine("Dependency one!");
        }
    }
}
