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
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Constructor)]
    public sealed class InjectAttribute : Attribute
    {
        private InjectMethod injectMethod;
        public InjectAttribute() { this.injectMethod = InjectMethod.None; }
        public InjectAttribute(InjectMethod injectMethod) { this.injectMethod = injectMethod; }

        public InjectMethod InjectMethod { get { return this.injectMethod; } }
    }
}
