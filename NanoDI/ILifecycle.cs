using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoDI
{
    public interface ILifecycle
    {
        void Initialize();
        void Destroy();
    }
}
