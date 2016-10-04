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
	class Component : IComponent
	{
	    List<ComponentField> fields = new List<ComponentField>();

		public Component(string name, Type type, Scope scope, List<ComponentField> fields)
		{
			Name = name;
			Type = type;
			Scope = scope;
			this.fields = fields;
		}

		public Component(string name, Type type, Scope scope)
		{
			Name = name;
			Type = type;
			Scope = scope;
		}

	    public string Name { get; private set; }
	    public Type Type { get; private set; }
	    public Scope Scope { get; private set; }

	    public List<ComponentField> Fields { get { return fields; } set { fields = value; } }


    }
}
