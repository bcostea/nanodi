using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoDI.Component
{
	class ComponentField
	{
		string name;

		public ComponentField(string name)
		{
			this.name = name;
		}

		public string Name { get { return name; } }

	}
}
