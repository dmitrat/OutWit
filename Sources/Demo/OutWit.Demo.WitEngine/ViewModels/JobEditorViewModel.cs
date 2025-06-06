using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;
using OutWit.Common.Aspects;
using OutWit.Common.Controls.HighlightTextBox.Highlighters;
using OutWit.Common.Controls.HighlightTextBox.HighlightRules;
using OutWit.Common.MVVM.Commands;
using OutWit.Common.MVVM.Utils;
using OutWit.Common.MVVM.ViewModels;
using OutWit.Common.Utils;
using OutWit.Demo.WitEngine.Utils;
using OutWit.Demo.WitEngine.Views;
using OutWit.Engine.Data.Jobs;
using OutWit.Engine.Data.Status;
using OutWit.Engine.Interfaces;
using OutWit.Themes.Interfaces;
using static OutWit.Common.MVVM.Utils.Extensions;

namespace OutWit.Demo.WitEngine.ViewModels
{
    public class JobEditorViewModel : ViewModelBase<ApplicationViewModel>
    {
        #region Constants

        private const string JOB_FILE_EXTENSION = "job";
        
        private const string JOB_EDITOR_SYNTAX = "JobEditorSyntax";

        #endregion

        #region Constructors

        public JobEditorViewModel(ApplicationViewModel applicationVm) :
            base(applicationVm)
        {
            InitDefaults();
            InitEvents();
            InitCommands();
        }

        #endregion

        #region Initialization

        private void InitDefaults()
        {
            HighlighterManager.Instance.LoadSyntaxXml(Properties.Resources.Syntax);
            var highlighter = HighlighterManager.Instance.Find(JOB_EDITOR_SYNTAX);

            AvailableVariables = ServiceLocator.Get.EngineManager.AvailableVariables;
            highlighter?.Add(new HighlightRuleWords
            {
                Words = AvailableVariables,
                FontStyle = FontStyles.Normal,
                FontWeight = FontWeights.Normal,
                Foreground = new SolidColorBrush(Color.FromRgb(0, 25, 255))
            });

            AvailableActivities = ServiceLocator.Get.EngineManager.AvailableActivities;
            highlighter?.Add(new HighlightRuleWords
            {
                Words = AvailableActivities,
                FontStyle = FontStyles.Normal,
                FontWeight = FontWeights.Normal,
                Foreground = new SolidColorBrush(Color.FromRgb(54, 168, 209))
            });
            
            UpdateStatus();
        }

        private void InitEvents()
        {
            ServiceLocator.Get.ThemeContainer.CurrentThemeChanged += OnCurrentThemeChanged;

            this.PropertyChanged += OnPropertyChanged;
        }

        private void InitCommands()
        {
            InsertVariableCmd = new DelegateCommand(x => InsertVariable());
            InsertActivityCmd = new DelegateCommand(x => InsertActivity());

            LoadJobCmd = new DelegateCommand(x => LoadJob());
            SaveJobCmd = new DelegateCommand(x => SaveJob());
            SaveJobAsCmd = new DelegateCommand(x => SaveJobAs());
            RunJobCmd = new DelegateCommand(x => RunJob());
        }

        #endregion

        #region Functions

        public void Navigate()
        {
            ServiceLocator.Get.NavigationManager.Navigate<JobEditor>();
        }

        private void InsertVariable()
        {
            if(string.IsNullOrEmpty(SelectedVariable))
                return;

            InsertValue($"{SelectedVariable}:");
        }

        private void InsertActivity()
        {
            if (string.IsNullOrEmpty(SelectedActivity))
                return;

            InsertValue($"{SelectedActivity}(");
        }

        private void InsertValue(string textToInsert)
        {
            var currentPosition = Position;

            if (Text == null)
                Text = textToInsert;
            else
                Text = Text.Insert(Position, textToInsert);

            Position = currentPosition + textToInsert.Length;
        }

        private void LoadJob()
        {
            var dialog = new OpenFileDialog
            {
                Filter = ServiceLocator.Get.Resources[nameof(Properties.Resources.LoadJobFiIter)]
            };
            
            if(dialog.ShowDialog() != true)
                return;

            try
            {
                Text = File.ReadAllText(dialog.FileName);
                JobFilePath = dialog.FileName;
            }
            catch (Exception e)
            {
                Text = "";
                JobFilePath = "";
            }
        }

        private async void SaveJob()
        {
            if(string.IsNullOrEmpty(Text))
                return;

            if (string.IsNullOrEmpty(JobFilePath))
                SaveJobAs();
            else
                await SaveJob(JobFilePath);

        }

        private async void SaveJobAs()
        {
            if (string.IsNullOrEmpty(Text))
                return;

            var dialog = new SaveFileDialog()
            {
                Filter = ServiceLocator.Get.Resources[nameof(Properties.Resources.LoadJobFiIter)]
            };

            if (dialog.ShowDialog() != true)
                return;

            await SaveJob(dialog.FileName);
        }

        private async void RunJob()
        {
            if(string.IsNullOrEmpty(Text))
            {
                Output = $"{ServiceLocator.Get.Resources["JobIsEmpty"]}";
                return;
            }

            WitJob job = null;
            try
            {
                job = ServiceLocator.Get.EngineManager.Compile(Text);
            }
            catch (Exception e)
            {
                Output = $"{ServiceLocator.Get.Resources["FailedToCompile"]}\n{e.Message}";
                return;
            }
            
            Output = await this.RunLongProcess("ProcessingHeader", (progress, cancelled) =>
            {
                var status = ServiceLocator.Get.EngineManager.RunFast(job);
                if (status != WitProcessingStatus.Completed)
                    return ServiceLocator.Get.EngineManager.Message;

                var output = ServiceLocator.Get.Resources["Completed"];
                if(ServiceLocator.Get.EngineManager.Return != null && ServiceLocator.Get.EngineManager.Return.Count > 0)
                {
                    output += ":";
                    foreach (var value in ServiceLocator.Get.EngineManager.Return)
                        output += $"\n{value}";
                    
                }

                return output;
            });
        }

        private async Task SaveJob(string filePath)
        {
            try
            {
                await File.WriteAllTextAsync(filePath, Text);
                JobFilePath = filePath;
            }
            catch (Exception e)
            {
                await this.ShowError<OkOptions>("FailedToSaveJobHeader", "FailedToSaveJobLine1");
            }
        }

        private void UpdateStatus()
        {
            CanLoadJob = true;
            CanSaveJob = !string.IsNullOrEmpty(Text);
            CanSaveJobAs = !string.IsNullOrEmpty(Text);
            CanRunJob = !string.IsNullOrEmpty(Text);
        }
        
        #endregion

        #region Event Handlers

        private void OnCurrentThemeChanged(ITheme currentTheme)
        {
            var text = Text;
            Text = "";
            Text = text;
        }

        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.IsProperty((JobEditorViewModel vm)=>vm.Text))
                UpdateStatus();

            if (e.IsProperty((JobEditorViewModel vm) => vm.JobFilePath))
                UpdateTitle();
        }

        private void UpdateTitle()
        {
            if(string.IsNullOrEmpty(JobFilePath))
                ServiceLocator.Get.WindowManager.ResetTitle();
            else
                ServiceLocator.Get.WindowManager.UpdateTitle(JobFilePath);
            
        }

        #endregion

        #region Properties

        [Notify]
        public string Output { get; set; }

        [Notify]
        public string Text { get; set; }

        [Notify]
        public string JobFilePath { get; private set; }

        [Notify]
        public int Position { get; set; }

        [Notify]
        public int SelectionStart { get; private set; }

        [Notify]
        public int SelectionLength { get; private set; }

        [Notify]
        public IReadOnlyList<string> AvailableVariables { get; private set; }

        [Notify]
        public string SelectedVariable { get; set; }

        [Notify]
        public IReadOnlyList<string> AvailableActivities { get; private set; }

        [Notify]
        public string SelectedActivity { get; set; }

        [Notify]
        public bool CanLoadJob { get; private set; }

        [Notify]
        public bool CanSaveJob { get; private set; }

        [Notify]
        public bool CanSaveJobAs { get; private set; }
        
        [Notify]
        public bool CanRunJob { get; private set; }

        #endregion

        #region Commands

        [Notify]
        public ICommand LoadJobCmd { get; private set; }

        [Notify]
        public ICommand SaveJobCmd { get; private set; }

        [Notify]
        public ICommand SaveJobAsCmd { get; private set; }

        [Notify]
        public ICommand RunJobCmd { get; private set; }

        [Notify]
        public ICommand InsertVariableCmd { get; private set; }

        [Notify]
        public ICommand InsertActivityCmd { get; private set; }

        #endregion
    }
}
