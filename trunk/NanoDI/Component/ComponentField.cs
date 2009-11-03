using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoDI.Component
{
	class ComponentField
	{
		string name;
		string value;

		public ComponentField(string name)
		{
			this.name = name;
		}

		public ComponentField(string name, string value)
		{
			this.name = name;
			this.value = value;
		}


		public string Name { get { return name; } }
		public string Value { get { return value; } }

	}
}
