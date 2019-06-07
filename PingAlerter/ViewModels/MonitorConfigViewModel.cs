using PingAlerter.Common;
using PingAlerter.Network;
using System;
using System.ComponentModel;

namespace PingAlerter.ViewModels
{

    public class MonitorConfigViewModel : BaseViewModel<Object>
    {
        private LatencyMonitorConfig Model;

        public MonitorConfigViewModel(LatencyMonitorConfig model)
        {
            this.Model = model; 
        }

        #region Exposing Properties of Model

        public int LatencyThreshold
        {
            get => Model.LatencyThreshold;
            set { Model.LatencyThreshold = value; OnPropertyChanged("LatencyThreshold"); }
        }

        public int StdDeviationThreshold
        {
            get { return Model.StdDeviationThreshold; }
            set { Model.StdDeviationThreshold = value; OnPropertyChanged("StdDeviationThreshold"); }
        }

        public int DefGatewayLatencyThreshold
        {
            get { return Model.DefGatewayLatencyThreshold; }
            set { Model.DefGatewayLatencyThreshold = value; OnPropertyChanged("DefGatewayLatencyThreshold"); }
        }

        public int DefGatewayStdDeviationThreshold
        {
            get { return Model.DefGatewayStdDeviationThreshold; }
            set { Model.DefGatewayStdDeviationThreshold = value; OnPropertyChanged("DefGatewayStdDeviationThreshold"); }
        }

        public int PreCheckAmountOfSamples
        {
            get { return Model.PreCheckAmountOfSamples; }
            set { Model.PreCheckAmountOfSamples = value; OnPropertyChanged("PreCheckAmountOfSamples"); }
        }

        public int PreCheckAmountOfPingsPerSample
        {
            get { return Model.PreCheckAmountOfPingsPerSample; }
            set { Model.PreCheckAmountOfPingsPerSample = value; OnPropertyChanged("PreCheckAmountOfPingsPerSample"); }
        }

        #endregion
    }
}
