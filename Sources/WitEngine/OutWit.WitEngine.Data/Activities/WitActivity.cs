using System;
using System.Runtime.Serialization;
using OutWit.WitEngine.Data.Attributes;
using OutWit.WitEngine.Interfaces;

namespace OutWit.WitEngine.Data.Activities
{
    [DataContract]
    public abstract class WitActivity : IWitActivity
    {
        #region Properties

        public virtual int StagesCount => 1;

        public virtual string Type => OperatorAttribute.GetOperatorType<ActivityAttribute>(GetType());

        #endregion

    }
}
