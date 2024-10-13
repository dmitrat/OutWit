using MaterialDesignThemes.Wpf;
using OutWit.Common.Aspects;
using OutWit.Common.Controls.Converters;
using OutWit.Common.Controls.Interfaces;
using OutWit.Common.Controls.Prompts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace OutWit.Demo.Services
{
    public class PopupManager : IPopupManager
    {
        #region Constants

        private const string DIALOG_IDENTIFIER = "RootDialog";

        #endregion

        #region Constructors

        public PopupManager()
        {
            InitDefaults();
        }

        #endregion

        #region Initialization

        private void InitDefaults()
        {
            MessageQueue = new SnackbarMessageQueue(ServiceLocator.Get.Settings.SnackBarMessageDuration)
            {
                DiscardDuplicates = true
            };
        }

        #endregion

        #region Snackbar

        public void ShowSnackBarInfo(string message)
        {
            MessageQueue.Enqueue(message, ServiceLocator.Get.Resources["Ok"], () => { });
        }

        #endregion

        #region Generic Prompt

        public async Task<TResult> ShowPrompt<TResult>(Control view)
        {
            try
            {
                ServiceLocator.Get.WindowManager.LockNavigation();

                var result = await DialogHost.Show(view, DIALOG_IDENTIFIER);
                return (TResult)result;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                ServiceLocator.Get.WindowManager.UnlockNavigation();
            }
        }
        public async Task<TResult> ShowPrompt<TResult>(Control view, DialogOpenedEventHandler onOpenDialog)
        {
            try
            {
                ServiceLocator.Get.WindowManager.LockNavigation();

                var result = await DialogHost.Show(view, DIALOG_IDENTIFIER, onOpenDialog);
                return (TResult)result;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                ServiceLocator.Get.WindowManager.UnlockNavigation();
            }

        }

        public void CloseDialog(object parameter)
        {
            DialogHost.Close(DIALOG_IDENTIFIER, parameter);
        }

        #endregion

        #region Info Prompt

        public async Task<TEnum> ShowInfo<TEnum, TConverter>(string header, params string[] lines)
            where TEnum : Enum
            where TConverter : StringToResourceConverterBase, new()
        {
            var options = Enum.GetValues(typeof(TEnum));

            var control = new InfoPrompt
            {
                Background = new SolidColorBrush(Themes.Selector.Get.ThemeContainer.Current.Paper),
                Foreground = new SolidColorBrush(Themes.Selector.Get.ThemeContainer.Current.Body),

                HeaderBackground = new SolidColorBrush(Themes.Selector.Get.ThemeContainer.Current.PrimaryDark),
                HeaderForeground = new SolidColorBrush(Themes.Selector.Get.ThemeContainer.Current.PrimaryDarkForeground),

                ButtonsForeground = new SolidColorBrush(Themes.Selector.Get.ThemeContainer.Current.PrimaryDark),

                Kind = PackIconKind.InformationCircleOutline,

                TextConverter = new TConverter(),
                HeaderKey = header,
                TextSource = lines,

                OptionSource = options,
                OptionDefault = options.OfType<TEnum>().First(),
                OptionCancel = options.OfType<TEnum>().Last()
            };

            return await ShowPrompt<TEnum>(control);
        }

        public async Task<TEnum> ShowError<TEnum, TConverter>(string header, params string[] lines)
            where TEnum : Enum
            where TConverter : StringToResourceConverterBase, new()
        {
            var options = Enum.GetValues(typeof(TEnum));

            var control = new InfoPrompt
            {
                Background = new SolidColorBrush(Themes.Selector.Get.ThemeContainer.Current.Paper),
                Foreground = new SolidColorBrush(Themes.Selector.Get.ThemeContainer.Current.Body),

                HeaderBackground = new SolidColorBrush(Themes.Selector.Get.ThemeContainer.Current.ValidationError),
                HeaderForeground = new SolidColorBrush(Themes.Selector.Get.ThemeContainer.Current.PrimaryDarkForeground),

                ButtonsForeground = new SolidColorBrush(Themes.Selector.Get.ThemeContainer.Current.PrimaryDark),

                Kind = PackIconKind.ErrorOutline,

                TextConverter = new TConverter(),
                HeaderKey = header,
                TextSource = lines,

                OptionSource = options,
                OptionDefault = options.OfType<TEnum>().First(),
                OptionCancel = options.OfType<TEnum>().Last()
            };

            return await ShowPrompt<TEnum>(control);
        }

        #endregion

        #region Process Prompt

        public async Task<TResult> RunLongProcess<TResult, TConverter>(string header, ProcessDelegate<TResult> process)
            where TConverter : StringToResourceConverterBase, new()
        {
            var control = new ProcessingPrompt { HeaderKey = header, TextConverter = new TConverter() };

            return await ShowPrompt<TResult>(control, (s, e) =>
            {
                Task.Run(() =>
                {
                    var processResult = process();
                    control.Dispatcher.Invoke(() =>
                    {
                        DialogHost.CloseDialogCommand.Execute(processResult, control);
                    });
                });
            });
        }

        #endregion

        #region Progress Prompt

        public async Task<TResult> RunLongProcess<TResult, TConverter>(string header, string button, ProgressDelegate<TResult> process)
            where TConverter : StringToResourceConverterBase, new()
        {
            var control = new ProgressPrompt
            {
                HeaderKey = header,
                ButtonTextKey = button,
                Width = 300,
                Minimum = 0,
                Maximum = 100,
                Value = 0,
                TextConverter = new TConverter()
            };

            return await ShowPrompt<TResult>(control, (s, e) =>
            {
                Task.Run(() =>
                {
                    var processResult = process(p =>
                    {
                        control.Dispatcher.InvokeAsync(() => control.Value = p);
                    }, () => control.IsCancelled);

                    control.Dispatcher.Invoke(() =>
                    {
                        DialogHost.CloseDialogCommand.Execute(processResult, control);
                    });
                });
            });
        }

        public async Task<TResult> RunLongProcess<TResult, TConverter>(string header, ProgressDelegate<TResult> process)
            where TConverter : StringToResourceConverterBase, new()
        {
            var control = new ProgressPrompt
            {
                HeaderKey = header,
                IsButtonVisible = false,
                Width = 300,
                Minimum = 0,
                Maximum = 100,
                Value = 0,
                TextConverter = new TConverter()
            };

            return await ShowPrompt<TResult>(control, (s, e) =>
            {
                Task.Run(() =>
                {
                    var processResult = process(p =>
                    {
                        control.Dispatcher.InvokeAsync(() => control.Value = p);
                    }, () => control.IsCancelled);

                    control.Dispatcher.Invoke(() =>
                    {
                        DialogHost.CloseDialogCommand.Execute(processResult, control);
                    });
                });
            });
        }

        #endregion

        #region Properties

        [Notify]
        public SnackbarMessageQueue MessageQueue { get; private set; }

        #endregion
    }
}
