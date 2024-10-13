using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OutWit.Engine.Data.Jobs;
using OutWit.Engine.Data.Status;

namespace OutWit.Engine.Data.Interfaces
{
    public interface IWItEngineManager
    {
        WitJob Compile(string jobString);

        WitProcessingStatus RunFast(WitJob job);
        
        string Output { get; }
        
        string Message { get; }

        IReadOnlyList<object> Return { get; }
        
        IReadOnlyList<string> AvailableActivities { get; }
        
        IReadOnlyList<string> AvailableVariables { get; }
    }
}
