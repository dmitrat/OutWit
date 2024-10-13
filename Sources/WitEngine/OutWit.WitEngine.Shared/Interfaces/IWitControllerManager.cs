using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OutWit.WitEngine.Data.Jobs;
using OutWit.WitEngine.Shared.Activities;
using OutWit.WitEngine.Shared.Jobs;
using OutWit.WitEngine.Shared.Variables;
using OutWit.WitEngine.Interfaces;

namespace OutWit.WitEngine.Shared.Interfaces
{
    public interface IWitControllerManager : IWitSerializationAdapter, IWitProcessingAdapter
    {
        void RegisterJob<TJob>(WitJobAdapterBase<TJob> adapter)
            where TJob : IWitJob;

        void RegisterVariable<TVariable>(WitVariableAdapterBase<TVariable> adapter)
            where TVariable : IWitVariable;

        void RegisterActivity<TActivity>(WitActivityAdapterBase<TActivity> adapter)
            where TActivity : IWitActivity;

        IWitProcessingAdapter Processor<TActivity>(TActivity activityObj)
            where TActivity : IWitActivity;

        IWitSerializationAdapter Serializer<TOperator>(TOperator operatorObj)
            where TOperator : IWitOperator;

        IWitSerializationAdapter Serializer(string operatorName);

        IWitProcessingAdapter Processor(string activityName);
    }
}
