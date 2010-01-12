using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ndi.Tooling.Properties.PropertyManager
{
	public interface IPropertyManager
	{
		string GetProperty(string propertyName);

		Boolean OSOverridesCustom { get; set; }
	}
}
