using System;
using System.Runtime.Serialization;
using OutWit.Engine.Data.Attributes;
using OutWit.Engine.Interfaces;

namespace OutWit.Engine.Data.Activities
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
