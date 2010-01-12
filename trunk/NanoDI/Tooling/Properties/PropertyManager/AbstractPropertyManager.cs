using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ndi.Tooling.Properties.PropertyManager
{
	public abstract class AbstractPropertyManager:IPropertyManager
	{

		public abstract string GetProperty(string propertyName);

		public bool OSOverridesCustom { get; set; }

	}
}
