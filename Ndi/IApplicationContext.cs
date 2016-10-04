/** 
 * This File is part of the NDI Library
 * Copyright 2009-2016 Bogdan COSTEA <bogdan@costea.us>
 * 
 * This library is free software, published under the terms of the LGPL version 2.1 or newer.
 * More info in the LICENSE.TXT file in the root of the project.
 * 
 */

namespace Ndi
{
	public interface IApplicationContext:ILifecycle 
	{
		void Initialize(string initializationParameter);
		object GetComponent(string componentName);

		Lifecycle Lifecycle { get; }
	}
}
