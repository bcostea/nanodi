using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoDI
{
	public interface IApplicationContext:ILifecycle 
	{
		void Initialize(string initializationParameter);
		object GetComponent(string componentName);

		Lifecycle Lifecycle { get; }
	}
}
