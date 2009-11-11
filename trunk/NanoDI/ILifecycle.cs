using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ndi
{
    public interface ILifecycle
    {
        void Initialize();
        void Destroy();
    }
}
