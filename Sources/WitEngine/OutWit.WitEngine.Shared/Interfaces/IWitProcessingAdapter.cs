using System;
using OutWit.WitEngine.Data.Variables;
using OutWit.WitEngine.Interfaces;

namespace OutWit.WitEngine.Shared.Interfaces
{
    public interface IWitProcessingAdapter
    {
        bool Process(IWitActivity activity, WitVariableCollection pool, out string message);
    }
}
