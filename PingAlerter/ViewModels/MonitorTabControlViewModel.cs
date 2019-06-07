using PingAlerter.Common;
using PingAlerter.IO.FileSystem;
using PingAlerter.Network;
using PingAlerter.Other.Log;
using PingAlerter.Other.MainWindow;
using PingAlerter.Other.MonitorConfig;
using PingAlerter.Other.MonitorTab;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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


namespace PingAlerter.ViewModels
{
    public class MonitorTabControlViewModel : BaseViewModel<int>
    {

        private System.Media.SoundPlayer player;
        private FileLogger filelogger;


        // ViewModels //
        //MainWindowViewModel mainWindowViewModel; // for general things such as status bar ( should I create a status bar viewModel? )
        public MonitorTabViewModel monitorTabViewModel { get; set; } // for monitor tab
        public MonitorConfigViewModel monitorConfigViewModel { get; set; } // for Settings tab
        public LogViewModel logViewModel { get; set; } // for Logs tab


        #region Constructor, dependancy injections and Members setup.
        public MonitorTabControlViewModel()
        {
            InitViewModels();
            InitClass();
        }
        
        private void InitClass()
        {
            player = new System.Media.SoundPlayer(@"D:\temp\Programs\Fiddler\LoadScript.wav");
            filelogger = new FileLogger(@"D:\LogFile.txt");

            Observer<MonitorServiceNotify> o = new Observer<MonitorServiceNotify>(
          (data) =>
          {
              switch (data.eventType)
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

              NotifyObservers(data.ScanCount);

              Debug.WriteLine("OBSERVER OF MainWindow, ScanCount: " + data.ScanCount);

          });

            monitorTabViewModel.Subscribe(o);

        }

        private void InitViewModels()
        {
            #region Log Tab
            // Log tab
            logViewModel = new LogViewModel();
            #endregion

            #region Settings Tab
            // Settings tab
            LatencyMonitorConfig monitorModel = new LatencyMonitorConfig();
            monitorConfigViewModel = new MonitorConfigViewModel(monitorModel);
            #endregion

            #region Monitor Tab
            // Monitor tab
            monitorTabViewModel = new MonitorTabViewModel();
            #endregion

            #region Experimental Tab
            // Experimental tab
            //ExperimentalViewModel experimentalViewModel = new ExperimentalViewModel();
            #endregion

        }
        #endregion
    }
}
