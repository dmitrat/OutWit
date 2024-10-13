using System.Collections.Generic;
using OutWit.Common.Exceptions;
using OutWit.Engine.Data.Attributes;
using OutWit.Engine.Data.Variables;
using OutWit.Engine.Shared.Activities;
using OutWit.Engine.Shared.Interfaces;
using OutWit.Engine.Shared.Jobs;
using OutWit.Engine.Shared.Utils;
using OutWit.Engine.Shared.Variables;
using OutWit.Engine.Interfaces;
using OutWit.Engine.Utils;

namespace OutWit.Engine.Services
{
    public class ControllerManager : IWitControllerManager
    {
        #region Fields

        private readonly Dictionary<string, IWitSerializationAdapter> m_serializers;
        private readonly Dictionary<string, IWitProcessingAdapter> m_processors;
        
        private readonly List<string> m_activities;
        private readonly List<string> m_variables;

        #endregion

        #region Constructors

        public ControllerManager()
        {
            m_serializers = new Dictionary<string, IWitSerializationAdapter>();
            m_processors = new Dictionary<string, IWitProcessingAdapter>();
            
            m_activities = new List<string>();
            m_variables = new List<string>();
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
            m_variables.Add(OperatorType<TVariable>());
        }

        public void RegisterActivity<TActivity>(WitActivityAdapterBase<TActivity> adapter)
            where TActivity : IWitActivity
        {
            m_serializers.Add(OperatorType<TActivity>(), adapter);
            m_processors.Add(OperatorType<TActivity>(), adapter);
            m_activities.Add(OperatorType<TActivity>());
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

            throw new ExceptionOf<IWitSerializationAdapter>($"{ServiceLocator.Get.Resources.UndefinedOperator}: {operatorName}");
        }

        public IWitProcessingAdapter Processor(string activityName)
        {
            if (m_processors.TryGetValue(activityName, out IWitProcessingAdapter processor))
                return processor;

            throw new ExceptionOf<IWitProcessingAdapter>($"{ServiceLocator.Get.Resources.UndefinedActivity}: {activityName}");
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
        
        public void Clear()
        {
            m_processors.Clear();
            m_serializers.Clear();
        }

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

        #region Properties

        public IReadOnlyList<string> AvailableActivities => m_activities;
        
        public IReadOnlyList<string> AvailableVariables => m_variables;

        #endregion


    }
}
