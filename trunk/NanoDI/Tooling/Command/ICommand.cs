using System.Collections.Generic;

namespace Ndi.Tooling.Command
{
    public interface ICommand    
    {
        void Execute();
        void Execute(Dictionary<string, object> context);
    }
}