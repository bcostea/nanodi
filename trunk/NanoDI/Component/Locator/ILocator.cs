using System;
using System.Collections.Generic;

namespace NanoDI.Component.Locator
{
    interface ILocator
    {
        List<IComponent> Locate();
    }
}
