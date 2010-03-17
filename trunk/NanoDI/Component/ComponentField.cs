using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ndi.Attributes;

namespace Ndi.Component
{
	class ComponentField
	{
		string name;
        InjectMethod injectMethod;


		public ComponentField(string name)
		{
			this.name = name;
		}

        public ComponentField(string name, InjectMethod injectMethod)
        {
            this.name = name;
            this.injectMethod = injectMethod;
        }


		public string Name { get { return name; } }

        public InjectMethod InjectMethod { get { return injectMethod; } }

	}
}
