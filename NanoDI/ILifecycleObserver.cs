using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ndi
{
    public interface ILifecycleObserver
    {
        
        void BeforeInitialize();
        void AfterInitialize();

        void BeforeDestroy();
        void AfterDestroy();

    }
}
