using System;
using System.Collections.Generic;
using System.Text;
using OutWit.Common.Interfaces;

namespace OutWit.WitEngine.Shared.Interfaces
{
    public interface IWitResources : IResources
    {
        string IncorrectInput { get; }

        string WrongActivityType { get; }

        string UndefinedOperator { get; }

        string UndefinedActivity { get; }
    }
}
