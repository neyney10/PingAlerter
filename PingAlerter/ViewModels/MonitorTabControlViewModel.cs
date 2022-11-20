using PingAlerter.Common;
using PingAlerter.IO;
using PingAlerter.IO.Database;
using PingAlerter.IO.FileSystem;
using PingAlerter.Models;
using PingAlerter.Network;
using PingAlerter.Other.Log;
using PingAlerter.Other.MonitorTab;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Input;

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
        public AboutViewModel aboutViewModel { get; set; } // for About tab
        public LogViewModel logViewModel { get; set; } // for Logs tab
        public LineChartViewModel chartsViewModel { get; set; } // for Charts tab
        #endregion

        #region Constructor, dependancy injections and Members setup.
        public MonitorTabControlViewModel()
        {
            alertConfig = new AlertConfigModel(@"D:\temp\Projects\C#\PingAlerter\PingAlerter\LoadScript.wav");
            logConfig = new LogConfigModel(@"D:\LogFile.txt", "server =127.0.0.1;uid=root;pwd=root;database=sys;Allow User Variables=True");

            InitViewModels();
            InitClass();
        }

        private void InitClass()
        {
            Observer<MonitorServiceNotify> o = new Observer<MonitorServiceNotify>(
                (data) =>
                {
                    if (data.Scan.Alerts.Count > 0)
                    {
                        if (alertConfig.IsSoundOn)
                            alertConfig.PlaySound();
                    }

                    logViewModel.AddLog(data.Scan);

                    try
                    {
                        logConfig.SaveLog(data.Scan);
                    }
                    catch (Exception ex)
                    {
                        Application.Current.Dispatcher.BeginInvoke((Action)delegate
                        {
                            monitorTabViewModel.startLatencyMonitorCommand.Abort();
                            ShowErrorMessageBox(ex.Message);
                        });
                    }
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

            #region About Tab
            // About tab
            this.aboutViewModel = new AboutViewModel();
            #endregion

            #region Charts Tab
            this.chartsViewModel = new LineChartViewModel();
            #endregion region


        }
        #endregion

        private void ShowErrorMessageBox(string text)
        {
            string caption = "Log Error";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Error;

            MessageBox.Show(text, caption, button, icon, MessageBoxResult.Yes);
        }


       

    }
}
