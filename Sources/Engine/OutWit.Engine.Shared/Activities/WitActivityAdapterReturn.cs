using System.IO;
using Microsoft.Extensions.Logging;
using OutWit.Engine.Shared.Interfaces;
using OutWit.Engine.Interfaces;

namespace OutWit.Engine.Shared.Activities
{
    public abstract class WitActivityAdapterReturn<TActivity> : WitActivityAdapterSingleOperator<TActivity>
        where TActivity : IWitActivity
    {
        #region Constructors

        protected WitActivityAdapterReturn(IWitProcessingManager processingManager, ILogger logger, IWitResources resources) :
            base(processingManager, logger, resources)
        {
        } 

        #endregion

        #region IWitActivityAdapter

        protected override string Serialize(TActivity activity, string prefix)
        {
            var parameters = SerializeParameters(activity);
            var writer = new StringWriter();
            writer.Write($"{prefix}");

            if (!string.IsNullOrEmpty(parameters[0]))
                writer.Write($"{parameters[0]} = ");
            
            writer.Write($"{activity.Type}");
            writer.Write("(");

            if (parameters.Length > 1)
            {
                writer.Write(parameters[1]);
                for (int i = 2; i < parameters.Length; i++)
                    writer.Write($", {parameters[i]}");
            }
            writer.Write(");");

            return writer.ToString();
        } 

        #endregion


    }
}
