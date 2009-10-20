using NanoDI.Attributes;
using System;

namespace NanoDI.Tooling.Command.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class CommandAttribute : ComponentAttribute
    {

        string key;
        string description;
        string helpText;

        public CommandAttribute(string name, string key, string description, string helpText) 
            : base(name, Scope.Prototype) 
        {
            this.key = key;
            this.description = description;
            this.helpText = helpText;
        }

        public string Key { get { return key; } }
        public string Description { get { return description; } }
        public string HelpText { get { return helpText; } }

    }
}
