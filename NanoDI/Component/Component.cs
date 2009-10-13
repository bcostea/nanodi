using System;
using NanoDI.Attributes;

namespace NanoDI.Component
{
    class Component : IComponent
    {
        Type type;
        Scope scope;
        string name;

        public Component(string name, Type type, Scope scope)
        {
            this.name = name;
            this.type = type;
            this.scope = scope;
        }
        
        public string Name{get{return name;}}
        public Type Type{get{return type;}}
        public Scope Scope{get{return scope;}}
        
    }
}
