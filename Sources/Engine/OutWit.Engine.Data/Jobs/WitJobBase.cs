using System.Collections.Generic;
using System.Runtime.Serialization;
using OutWit.Common.Abstract;
using OutWit.Engine.Data.Activities;
using OutWit.Engine.Data.Variables;
using OutWit.Engine.Interfaces;

namespace OutWit.Engine.Data.Jobs
{
    public class WitJobBase : WitActivity, IWitJob
    {
        #region Constructors

        public WitJobBase()
        {
            Activities = new WitActivityCollection();
            Variables = new WitVariableCollection();
        }

        #endregion

        #region IWMJob

        void IWitJob.AddActivity(IWitActivity activity)
        {
            Activities.Add(activity);
        }

        void IWitJob.AddVariable(IWitVariable variable)
        {
            Variables.Add(variable);
        }

        IEnumerable<IWitActivity> IWitJob.Activities => Activities;
        IEnumerable<IWitVariable> IWitJob.Variables => Variables;

        #endregion

        #region Functions

        public virtual void UpdateParameters(params object[] parameters)
        {

        }

        #endregion

        #region Properties

        public WitActivityCollection Activities { get; set; }
        
        public WitVariableCollection Variables { get; set; }

        #endregion

    }
}
