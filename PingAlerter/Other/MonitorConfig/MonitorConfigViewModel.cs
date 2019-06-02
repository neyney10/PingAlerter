﻿using System.ComponentModel;

namespace PingAlerter.Other.MonitorConfig
{

    public class MonitorConfigViewModel : INotifyPropertyChanged
    {
        private MonitorConfigModel Model;

        public MonitorConfigViewModel(MonitorConfigModel model)
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

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
        }
    }
}