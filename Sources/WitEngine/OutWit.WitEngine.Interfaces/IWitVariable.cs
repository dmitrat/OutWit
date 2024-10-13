using System;

namespace OutWit.WitEngine.Interfaces
{
    public interface IWitVariable : IWitOperator, IWitNamed
    {
        object Value { get; set; }
    }
}
