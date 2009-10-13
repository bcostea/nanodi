using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NanoDI.Component.Cache
{
    interface IComponentCache
    {
        Boolean Contains(IComponent component);
        object Get(IComponent component);
        void Put(IComponent componentDefinition, object componentInstance);
        void Remove(IComponent component);
    }
}
