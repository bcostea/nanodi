﻿/** 
 * This File is part of the NDI Library
 * Copyright 2009,2010 Bogdan COSTEA <bogdan.costea@gridpulse.com>
 * 
 * This library is free software, published under the terms of the LGPL version 2.1 or newer.
 * More info in the LICENSE.TXT file in the root of the project.
 * 
 */

using System;

namespace Ndi.Component.Cache
{
    interface IComponentCache
    {
        Boolean Contains(IComponent component);
        object Get(IComponent component);
        void Put(IComponent componentDefinition, object componentInstance);
        void Remove(IComponent component);
        void Clear();
    }
}