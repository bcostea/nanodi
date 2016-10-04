/** 
 * This File is part of the NDI Library
 * Copyright 2009-2016 Bogdan COSTEA <bogdan@costea.us>
 * 
 * This library is free software, published under the terms of the LGPL version 2.1 or newer.
 * More info in the LICENSE.TXT file in the root of the project.
 * 
 */
using Ndi.Attributes;

namespace Ndi.Component
{
    /// <summary>
    /// Represents 
    /// </summary>
	public class ComponentField
	{
	    public ComponentField(string name, InjectMethod injectMethod)
        {
            Name = name;
            InjectMethod = injectMethod;
        }

	    public string Name { get; private set; }
        public string Value { get; set; }

	    public InjectMethod InjectMethod { get; private set; }
    }
}
