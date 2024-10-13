using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutWit.Engine.Interfaces
{
    public interface IWitJob : IWitOperator
    {
        void AddActivity(IWitActivity activity);
        void AddVariable(IWitVariable variable);

        IEnumerable<IWitActivity> Activities { get; }

        IEnumerable<IWitVariable> Variables { get; }
    }
}
