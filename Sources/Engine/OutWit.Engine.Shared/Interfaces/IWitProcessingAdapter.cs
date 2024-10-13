using System;
using OutWit.Engine.Data.Variables;
using OutWit.Engine.Interfaces;

namespace OutWit.Engine.Shared.Interfaces
{
    public interface IWitProcessingAdapter
    {
        bool Process(IWitActivity activity, WitVariableCollection pool, out string message);
    }
}
