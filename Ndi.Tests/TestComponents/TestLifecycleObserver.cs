using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ndi;

namespace NdiUnitTests.TestComponents
{
	class TestLifecycleObserver:ILifecycleObserver
	{
		public static int Initial = -1;
		public static int BeforeInit = 0;
		public static int AfterInit = 1;
		public static int BeforeDest = 2;
		public static int AfterDest = 3;

		int currentState = Initial;

		
		public void BeforeInitialize()
		{
			currentState = BeforeInit;
		}

		public void AfterInitialize()
		{
			currentState = AfterInit;
		}

		public void BeforeDestroy()
		{
			currentState = BeforeDest;
		}

		public void AfterDestroy()
		{
			currentState = AfterDest;	
		}

		public int State { get { return currentState; } }
	}
}
