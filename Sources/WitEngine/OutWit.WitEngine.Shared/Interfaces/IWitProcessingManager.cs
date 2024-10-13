using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutWit.WitEngine.Shared.Interfaces
{
    public interface IWitProcessingManager
    {
        void Resume();
        void Pause(string message);
        void ReportProgress(string message);
        void Return(object[] value);

        void LockProgress();
        void UnlockProgress();

        bool IsCancelled { get; }
        bool IsPaused { get; }
    }
}
