using PingAlerter.Common;
using PingAlerter.Network;
using System;
using System.ComponentModel;

namespace PingAlerter.Other.MonitorConfig
{

    public class MonitorConfigViewModel : BaseViewModel<Object>
    {
        private LatencyMonitorConfig Model;

        public MonitorConfigViewModel(LatencyMonitorConfig model)
        {
            this.Model = model; 
        }

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

    }
}
