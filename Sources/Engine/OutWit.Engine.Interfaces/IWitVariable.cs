using System;

namespace OutWit.Engine.Interfaces
{
    public interface IWitVariable : IWitOperator, IWitNamed
    {
        object Value { get; set; }
    }
}
