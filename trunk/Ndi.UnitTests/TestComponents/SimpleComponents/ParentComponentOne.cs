using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ndi.Attributes;

namespace Ndi.UnitTests.TestComponents.SimpleComponents
{
    [Component("parentComponentOne")]
    class ParentComponentOne:IParentComponent
    {
		[Inject]
        IChildComponent singletonComponent = null;
		[Inject]
		IChildComponent prototypeComponent = null;

		string dummyField = "this is a non injectable field";

        public string SingletonContent
        {
            get { return singletonComponent.Content; }
            set { singletonComponent.Content = value; }
        }

        public string PrototypeContent
        {
            get { return prototypeComponent.Content; }
            set { prototypeComponent.Content = value; }
        }

		public string Dummy { get { return dummyField; } }
    }
}
