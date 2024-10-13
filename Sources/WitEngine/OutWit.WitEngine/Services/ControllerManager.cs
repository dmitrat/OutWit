using System.Collections.Generic;
using OutWit.Common.Exceptions;
using OutWit.WitEngine.Data.Attributes;
using OutWit.WitEngine.Data.Variables;
using OutWit.WitEngine.Shared.Activities;
using OutWit.WitEngine.Shared.Interfaces;
using OutWit.WitEngine.Shared.Jobs;
using OutWit.WitEngine.Shared.Utils;
using OutWit.WitEngine.Shared.Variables;
using OutWit.WitEngine.Interfaces;
using OutWit.WitEngine.Utils;

namespace OutWit.WitEngine.Services
{
    public class ControllerManager : IWitControllerManager
    {
        #region Fields

        private readonly Dictionary<string, IWitSerializationAdapter> m_serializers;
        private readonly Dictionary<string, IWitProcessingAdapter> m_processors;

        #endregion

        #region Constructors

        public ControllerManager()
        {
            m_serializers = new Dictionary<string, IWitSerializationAdapter>();
            m_processors = new Dictionary<string, IWitProcessingAdapter>();
        }

        #endregion

        #region IWitControllerManager

        public void RegisterJob<TJob>(WitJobAdapterBase<TJob> adapter)
            where TJob : IWitJob
        {
            m_serializers.Add(OperatorType<TJob>(), adapter);
        }

        public void RegisterVariable<TVariable>(WitVariableAdapterBase<TVariable> adapter)
            where TVariable : IWitVariable
        {
            m_serializers.Add(OperatorType<TVariable>(), adapter);
        }

        public void RegisterActivity<TActivity>(WitActivityAdapterBase<TActivity> adapter)
            where TActivity : IWitActivity
        {
            m_serializers.Add(OperatorType<TActivity>(), adapter);
            m_processors.Add(OperatorType<TActivity>(), adapter);
        }

        public IWitProcessingAdapter Processor<TActivity>(TActivity activityObj)
            where TActivity : IWitActivity
        {
            return Processor(OperatorType(activityObj));
        }

        public IWitSerializationAdapter Serializer<TOperator>(TOperator operatorObj)
            where TOperator : IWitOperator
        {
            return Serializer(OperatorType(operatorObj));
        }

        public IWitSerializationAdapter Serializer(string operatorName)
        {
            if (m_serializers.TryGetValue(operatorName, out IWitSerializationAdapter serializer))
                return serializer;

            throw new ExceptionOf<IWitSerializationAdapter>(ServiceLocator.Get.Resources.UndefinedActivity);
        }

        public IWitProcessingAdapter Processor(string activityName)
        {
            if (m_processors.TryGetValue(activityName, out IWitProcessingAdapter processor))
                return processor;

            throw new ExceptionOf<IWitProcessingAdapter>(ServiceLocator.Get.Resources.UndefinedActivity);
        }

        #endregion

        #region IWitSerializerAdapter

        public string Serialize(IWitOperator activity, string prefix)
        {
            return Serializer(activity).Serialize(activity, prefix);
        }

        public void Deserialize(string activityStr, IWitJob job)
        {
            activityStr = activityStr.TrimAll();
            Serializer(activityStr.FindOperatorKey()).Deserialize(activityStr, job);
        }

        public IWitOperator Clone(IWitOperator obj)
        {
            return Serializer(obj).Clone(obj);
        }

        #endregion

        #region IWitProcessingAdapter

        public bool Process(IWitActivity activity, WitVariableCollection pool, out string message)
        {
            return Processor(activity).Process(activity, pool, out message);
        }

        #endregion

        #region Functions

        private string OperatorType(IWitOperator operatorObj)
        {
            return OperatorAttribute.GetOperatorType<OperatorAttribute>(operatorObj.GetType());
        }

        private string OperatorType<TOperator>()
            where TOperator : IWitOperator
        {
            return OperatorAttribute.GetOperatorType<OperatorAttribute>(typeof(TOperator));
        }

        #endregion

        
    }
}
