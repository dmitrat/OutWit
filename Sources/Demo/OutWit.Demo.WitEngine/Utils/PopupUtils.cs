using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OutWit.Common.Controls.Interfaces;
using OutWit.Common.MVVM.ViewModels;
using OutWit.Demo.WitEngine.Converters;
using OutWit.Demo.WitEngine.ViewModels;

namespace OutWit.Demo.WitEngine.Utils
{
    public static class PopupUtils
    {
        public static async Task<TEnum> ShowError<TEnum>(this ViewModelBase<ApplicationViewModel> me, string header, params string[] lines)
            where TEnum : Enum
        {
            return await ServiceLocator.Get.PopupManager.ShowError<TEnum, StringToResourceConverter>(header, lines);
        }

        public static async Task<TEnum> ShowInfo<TEnum>(this ViewModelBase<ApplicationViewModel> me, string header, params string[] lines)
            where TEnum : Enum
        {
            return await ServiceLocator.Get.PopupManager.ShowInfo<TEnum, StringToResourceConverter>(header, lines);
        }

        public static async Task<TResult> RunLongProcess<TResult>(this ViewModelBase<ApplicationViewModel> me, string header, ProcessDelegate<TResult> process)
        {
            return await ServiceLocator.Get.PopupManager.RunLongProcess<TResult, StringToResourceConverter>(header, process);
        }

        public static async Task<TResult> RunLongProcess<TResult>(this ViewModelBase<ApplicationViewModel> me, string header, string button, ProgressDelegate<TResult> process)
        {
            return await ServiceLocator.Get.PopupManager.RunLongProcess<TResult, StringToResourceConverter>(header, button, process);
        }

        public static async Task<TResult> RunLongProcess<TResult>(this ViewModelBase<ApplicationViewModel> me, string header, ProgressDelegate<TResult> process)
        {
            return await ServiceLocator.Get.PopupManager.RunLongProcess<TResult, StringToResourceConverter>(header, process);
        }
    }
}
