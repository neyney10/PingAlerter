using PingAlerter.Common;
using PingAlerter.IO;
using PingAlerter.IO.Database;
using PingAlerter.IO.FileSystem;
using PingAlerter.Models;
using PingAlerter.Network;
using PingAlerter.Other.Log;
using PingAlerter.Other.MonitorTab;

namespace PingAlerter.ViewModels
{
    public class MonitorTabControlViewModel : BaseViewModel<int>
    {
        #region Private Models and data members
        private AlertConfigModel alertConfig;
        private LogConfigModel logConfig;
        //private ILogger DBLogger;
        #endregion

        #region ViewModels
        // ViewModels //
        public MonitorTabViewModel monitorTabViewModel { get; set; } // for monitor tab
        public MonitorConfigViewModel monitorConfigViewModel { get; set; } // for Settings tab
        public LogViewModel logViewModel { get; set; } // for Logs tab
        #endregion

        #region Constructor, dependancy injections and Members setup.
        public MonitorTabControlViewModel()
        {
            alertConfig = new AlertConfigModel(@"D:\temp\Projects\C#\PingAlerter\PingAlerter\LoadScript.wav");
            logConfig = new LogConfigModel(@"D:\LogFile.txt");
            // TEST
            //DBLogger = new DBMySQLLogger("Server=127.0.0.1;Database=pingalerterlogs;Uid=root;Pwd=toor;");

            InitViewModels();
            InitClass();
        }
        
        private void InitClass()
        {
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
                      if(alertConfig.IsSoundOn)
                         alertConfig.PlaySound();
                      break;
              }

              logConfig.SaveLog(data.Data);

              NotifyObservers(data.ScanCount);

          });

            monitorTabViewModel.Subscribe(o);

        }

        private void InitViewModels()
        {
            LatencyMonitorConfig monitorConfigModel = new LatencyMonitorConfig();
            
            SettingsModel settings = new SettingsModel(monitorConfigModel, alertConfig, logConfig);

            #region Log Tab
            // Log tab
            logViewModel = new LogViewModel();
            #endregion

            #region Settings Tab
            // Settings tab
            monitorConfigViewModel = new MonitorConfigViewModel(settings);
            #endregion

            #region Monitor Tab
            // Monitor tab
            monitorTabViewModel = new MonitorTabViewModel(settings.monitorConfig); // notice the same 'settings' object as before in order to synchronize them.
            #endregion

            #region Experimental Tab
            // Experimental tab
            //ExperimentalViewModel experimentalViewModel = new ExperimentalViewModel();
            #endregion

        }
        #endregion
    }
}
