using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
