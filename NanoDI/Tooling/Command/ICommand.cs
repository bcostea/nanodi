using System.Collections.Generic;

namespace NanoDI.Tooling.Command
{
    public interface ICommand    
    {
        void Execute();
        void Execute(Dictionary<string, object> context);
    }
}