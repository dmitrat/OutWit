using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using OutWit.Common.Serialization;
using OutWit.WitEngine.Data.Jobs;
using OutWit.WitEngine.Data.Variables;
using OutWit.WitEngine.Shared;
using OutWit.WitEngine.Shared.Utils;
using OutWit.WitEngine.Interfaces;

namespace OutWit.WitEngine.Utils
{
    public static class WitEngineUtils
    {
        #region IWMSeriazlizerAdapter

        public static string Serialize(this IWitOperator me, string prefix)
        {
            return ServiceLocator.Get.ControllerManager.Serialize(me, prefix);
        }

        public static void Deserialize(this string me, IWitJob job)
        {
            ServiceLocator.Get.ControllerManager.Deserialize(me, job);
        }

        public static string Serialize(this IWitJob me)
        {
            return me.Serialize("");
        }

        public static WitJob Deserialize(this string me)
        {
            var job = new WitJobBase();

            me.Deserialize(job);

            return (WitJob)job.Activities.Single();
        }

        public static IWitOperator Clone(this IWitOperator obj)
        {
            return ServiceLocator.Get.ControllerManager.Clone(obj);
        }

        #endregion

        #region IWMProcessingAdapter

        public static bool Process(this IWitActivity me, WitVariableCollection pool, out string message)
        {
            return ServiceLocator.Get.ControllerManager.Process(me, pool, out message);
        }

        #endregion

        #region Functions

        public static IWitJob OpenJob(string path)
        {
            return Serialization.ReadTextFile(path).Deserialize();
        }

        public static bool SaveJob(this IWitJob me, string path)
        {
            return me.Serialize().ToFile(path);
        }

        #endregion

        #region Utils

        public static string FindOperatorKey(this string me)
        {
            var start = me.IndexOf('=') + 1;

            var openBracket = me.IndexOf('(');
            if (openBracket == -1)
                openBracket = me.Length;

            var colon = me.IndexOf(':');
            if (colon == -1)
                colon = me.Length;

            var end = Math.Min(colon, openBracket) - 1;

            if (start >= end)
                start = 0;

            var operatorKey = me.Between(start, end);

            return operatorKey == "Job" ? me.FindJobKey() : operatorKey;
        }


        private static string FindJobKey(this string me)
        {
            var openBracket = me.IndexOf('(');
            var closeBracket = me.IndexOf(')');

            var jobKey = "Job(";

            var jobParams = me.Between(openBracket + 1, closeBracket - 1).SplitOperators(",");
            for (int i = 0; i < jobParams.Length; i++)
            {
                var jobParam = jobParams[i];

                var paramType = jobParam.To(jobParam.IndexOf(':') - 1);
                jobKey += paramType;

                if (i < jobParams.Length - 1)
                    jobKey += ',';
            }

            jobKey += ")";

            return jobKey;
        }

        #endregion

        #region BackgroundWorker

        public static void ReportProgress(this BackgroundWorker me, string message)
        {
            me.ReportProgress(0, message);
        }

        public static bool IsProgress(this ProgressChangedEventArgs me)
        {
            return me.ProgressPercentage == 0;
        }

        public static void ReturnValue(this BackgroundWorker me, object value)
        {
            me.ReportProgress(1, value);
        }

        public static bool IsReturn(this ProgressChangedEventArgs me)
        {
            return me.ProgressPercentage == 1;
        }

        #endregion
    }
}
