using PingAlerter.IO.FileSystem;
using PingAlerter.Network;
using PingAlerter.Other.Log;
using PingAlerter.Other.MainWindow;
using PingAlerter.Other.MonitorConfig;
using PingAlerter.Other.MonitorTab;
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

        private System.Media.SoundPlayer player;
        Thread Monitor; //temp
        private FileLogger filelogger;
  
        // Control models and data sources
        private bool StartButtonOn = false;

        // ViewModels //
        MainWindowViewModel mainWindowViewModel; // for general things such as status bar ( should I create a status bar viewModel? )
        MonitorTabViewModel monitorTabViewModel; // for monitor tab
        MonitorConfigViewModel monitorConfigViewModel; // for Settings tab
        LogViewModel logViewModel; // for Logs tab


        public MainWindow()
        {
            InitializeComponent();
            InitClass();
        }

        public void InitClass()
        {
            player = new System.Media.SoundPlayer(@"D:\temp\Programs\Fiddler\LoadScript.wav");
            filelogger = new FileLogger(@"D:\LogFile.txt");

            // General window
            this.DataContext = this;
            MainWindowModel mainWindowModel = new MainWindowModel();
            mainWindowViewModel = new MainWindowViewModel(mainWindowModel);

            stbar_label_scansvalue.DataContext = mainWindowViewModel;
            // Log tab
            logViewModel = new LogViewModel();

            datagrid_logs.DataContext = logViewModel;



            // Settings tab
            LatencyMonitorConfig monitorModel = new LatencyMonitorConfig();
            monitorConfigViewModel= new MonitorConfigViewModel(monitorModel);

            tab_settings.DataContext = monitorConfigViewModel;

            // Monitor tab
            monitorTabViewModel = new MonitorTabViewModel(logViewModel);

            tab_monitor.DataContext = monitorTabViewModel;

            MonitorObserver o = new MonitorObserver((data)=> 
            {
                switch(data.eventType)
                {
                    case MonitorServiceNotify.Type.Log:
                        logViewModel.AddLog(data.Data);
                        break;
                    case MonitorServiceNotify.Type.OverThreshold:
                        logViewModel.AddLog(data.Data);
                        player.Play();
                        break;
                }

                filelogger.WriteSingle(data.Data);
                mainWindowViewModel.ScanCount = data.ScanCount;

                Debug.WriteLine("OBSERVER OF MainWindow, ScanCount: "+ data.ScanCount);
                
            });

            monitorTabViewModel.Subscribe(o);

            // Experimental tab
            ExperimentalViewModel experimentalViewModel = new ExperimentalViewModel();

            Btn_experimental.DataContext = experimentalViewModel;


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


   

        class MonitorObserver : IObserver<MonitorServiceNotify>
        {
            private Action<MonitorServiceNotify> Callback;

            public MonitorObserver(Action<MonitorServiceNotify> callback)
            {
                this.Callback = callback;
            }

            public void OnCompleted()
            {
                throw new NotImplementedException();
            }

            public void OnError(Exception error)
            {
                throw new NotImplementedException();
            }

            public void OnNext(MonitorServiceNotify value)
            {
                this.Callback(value);
            }
        }


        public class ExperimentalViewModel
        {

            public ICommand Btn_experimental_OnClick { get; set; }

            public ExperimentalViewModel()
            {
                this.Btn_experimental_OnClick = new ExperimentalCommand();
            }

            public class ExperimentalCommand : ICommand
            {
                public event EventHandler CanExecuteChanged;

                public bool CanExecute(object parameter)
                {
                    return true;
                }

                public void Execute(object parameter)
                {
                    new Window().Show();
                }
            }
        }

    }
}
