using PingAlerter.Common;
using PingAlerter.IO.FileSystem;
using PingAlerter.Network;
using PingAlerter.Other.Log;
using PingAlerter.Other.MainWindow;
using PingAlerter.Other.MonitorConfig;
using PingAlerter.Other.MonitorTab;
using PingAlerter.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PingAlerter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowModel mainWindowViewModel;
        MonitorTabControlViewModel monitorTabControlViewModel;

        public MainWindow()
        {
            InitializeComponent();
            InitViewModels();
            InitClass();
        }

        private void InitClass()
        {
            Observer<int> TabControlObserver = new Observer<int>(
                (data)=>
                {
                    mainWindowViewModel.ScanCount = data;
                }
                );
        }

        private void InitViewModels()
        {
            mainWindowViewModel = new MainWindowModel();
            stbar_label_scansvalue.DataContext = mainWindowViewModel;

            monitorTabControlViewModel = new MonitorTabControlViewModel();
            MonitorTabControl.DataContext = monitorTabControlViewModel;
        }


        private void Window_StateChanged(object sender, EventArgs e)
        {
            switch (this.WindowState)
            {
                case WindowState.Maximized:
                    // TODO: create sperated method
                    Hide();
                    break;
                case WindowState.Minimized:
                    MessageBox.Show("Minimized");
                    break;
                case WindowState.Normal:
                    MessageBox.Show("Normal");
                    break;
            }
        }


    }
}
