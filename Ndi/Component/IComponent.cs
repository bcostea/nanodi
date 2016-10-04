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
using Ndi.Attributes;

namespace Ndi.Component
{
    public interface IComponent
    {
        string Name { get; }
        Type Type { get; }
        Scope Scope { get; }

		List<ComponentField> Fields { get; set; }

    }
}
