using PingAlerter.Common;
using PingAlerter.Models;
using PingAlerter.Network;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace PingAlerter.ViewModels
{

    public class MonitorConfigViewModel : BaseViewModel<Object>
    {
        #region Models
        private SettingsModel settings;
        #endregion

        #region Constructor
        public MonitorConfigViewModel(SettingsModel settings)
        {
            // initialize models - dependancy injection.
            this.settings = settings;

            // initialize commands.
            this.BrowseLogFLCmd = new RelayCommand((obj) => 
            {
                LogFilepath = OpenFileDialog();
            });

            this.BrowseAlertSoundFLCmd = new RelayCommand((obj) =>
            {
                SoundFilepath = OpenFileDialog();
            });
        }
        #endregion

        #region Exposing Properties of Model

        #region Monitor Configuration
        public int LatencyThreshold
        {
            get => settings.monitorConfig.LatencyThreshold;
            set { settings.monitorConfig.LatencyThreshold = value; OnPropertyChanged("LatencyThreshold"); }
        }

        public int StdDeviationThreshold
        {
            get { return settings.monitorConfig.StdDeviationThreshold; }
            set { settings.monitorConfig.StdDeviationThreshold = value; OnPropertyChanged("StdDeviationThreshold"); }
        }

        public int DefGatewayLatencyThreshold
        {
            get { return settings.monitorConfig.DefGatewayLatencyThreshold; }
            set { settings.monitorConfig.DefGatewayLatencyThreshold = value; OnPropertyChanged("DefGatewayLatencyThreshold"); }
        }

        public int DefGatewayStdDeviationThreshold
        {
            get { return settings.monitorConfig.DefGatewayStdDeviationThreshold; }
            set { settings.monitorConfig.DefGatewayStdDeviationThreshold = value; OnPropertyChanged("DefGatewayStdDeviationThreshold"); }
        }

        public int PreCheckAmountOfSamples
        {
            get { return settings.monitorConfig.PreCheckAmountOfSamples; }
            set { settings.monitorConfig.PreCheckAmountOfSamples = value; OnPropertyChanged("PreCheckAmountOfSamples"); }
        }

        public int PreCheckAmountOfPingsPerSample
        {
            get { return settings.monitorConfig.PreCheckAmountOfPingsPerSample; }
            set { settings.monitorConfig.PreCheckAmountOfPingsPerSample = value; OnPropertyChanged("PreCheckAmountOfPingsPerSample"); }
        }
        #endregion

        #region Alert Configuration

        public bool IsSoundOn
        {
            get { return settings.alertConfigModel.IsSoundOn; }
            set { settings.alertConfigModel.IsSoundOn = value; OnPropertyChanged("IsSoundOn"); }
        }

        public string SoundFilepath
        {
            get { return settings.alertConfigModel.SoundFilepath; }
            set { settings.alertConfigModel.SoundFilepath = value; OnPropertyChanged("SoundFilepath"); }
        }

        #endregion

        #region Log Configuration

        public string LogFilepath
        {
            get { return settings.logConfigModel.Filepath; }
            set { settings.logConfigModel.Filepath = value; OnPropertyChanged("LogFilepath"); }
        }

        #endregion

        #endregion

        #region Commands

        public ICommand BrowseLogFLCmd { get; set; }
        public ICommand BrowseAlertSoundFLCmd { get; set; }

        #endregion

        public string OpenFileDialog()
        {
            #region Debug logging
            Debug.WriteLine("Opening File Location Browser... (Command)");
            #endregion

            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.CheckFileExists = false;
            bool? result = openFileDialog.ShowDialog();

            // on success: user chose a file location + name.
            if (result == true)
            {
                #region Debug logging
                Debug.WriteLine("FileLocation: " + openFileDialog.FileName + " (Command)");
                #endregion

                return openFileDialog.FileName;
            }

            return null;
        }
    }
}
