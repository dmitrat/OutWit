﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OutWit.Demo.WitEngine.ViewModels;

namespace OutWit.Demo.WitEngine.Views
{
    /// <summary>
    /// Interaction logic for JobEditorSidePanel.xaml
    /// </summary>
    public partial class JobEditorSidePanel : UserControl
    {
        public JobEditorSidePanel()
        {
            InitializeComponent();
            DataContext = ApplicationViewModel.Instance;
        }
    }
}