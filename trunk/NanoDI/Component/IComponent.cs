using System;
using NanoDI.Attributes;

namespace NanoDI.Component
{
    interface IComponent
    {
        string Name { get; }
        Type Type { get; }
        Scope Scope { get; }
    }
}
