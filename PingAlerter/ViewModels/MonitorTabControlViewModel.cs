using PingAlerter.Common;
using PingAlerter.IO.FileSystem;
using PingAlerter.Network;
using PingAlerter.Other.Log;
using PingAlerter.Other.MonitorTab;

namespace PingAlerter.ViewModels
{
    public class MonitorTabControlViewModel : BaseViewModel<int>
    {
        private System.Media.SoundPlayer player;
        private FileLogger filelogger;

        // ViewModels //
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
            LatencyMonitorConfig monitorConfigModel = new LatencyMonitorConfig();
            monitorConfigViewModel = new MonitorConfigViewModel(monitorConfigModel);
            #endregion

            #region Monitor Tab
            // Monitor tab
            monitorTabViewModel = new MonitorTabViewModel(monitorConfigModel); // notice the same 'monitorConfigModel' object as before in order to synchronize them.
            #endregion

            #region Experimental Tab
            // Experimental tab
            //ExperimentalViewModel experimentalViewModel = new ExperimentalViewModel();
            #endregion

        }
        #endregion
    }
}
