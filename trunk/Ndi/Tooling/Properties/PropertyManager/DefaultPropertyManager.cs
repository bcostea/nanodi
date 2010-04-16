/** 
 * This File is part of the NDI Library
 * Copyright 2009,2010 Bogdan COSTEA <bogdan.costea@gridpulse.com>
 * 
 * This library is free software, published under the terms of the LGPL version 2.1 or newer.
 * More info in the LICENSE.TXT file in the root of the project.
 * 
 */

namespace Ndi.Tooling.Properties.PropertyManager
{
	class DefaultPropertyManager : AbstractPropertyManager
	{

		DefaultPropertyManager()
		{

		}

		public override string GetProperty(string propertyName)
		{
			return propertyName;
		}

	}
}
