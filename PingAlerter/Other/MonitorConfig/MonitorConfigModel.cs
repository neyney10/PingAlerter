using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics;

namespace PingAlerter.Other.MonitorConfig
{
    public class MonitorConfigModel 
    {
        private int latencyThreshold;
        public int LatencyThreshold
        {
            get => latencyThreshold;
            set
            { latencyThreshold = value; }
        }

        private int stdDeviationThreshold;
        public int StdDeviationThreshold
        {
            get => stdDeviationThreshold;
            set
            { stdDeviationThreshold = value; }
        }

        private int defGatewayLatencyThreshold;
        public int DefGatewayLatencyThreshold
        {
            get => defGatewayLatencyThreshold;
            set
            { defGatewayLatencyThreshold = value; }
        }

        private int defGatewayStdDeviationThreshold;
        public int DefGatewayStdDeviationThreshold
        {
            get => defGatewayStdDeviationThreshold;
            set
            { defGatewayStdDeviationThreshold = value; }
        }

        public MonitorConfigModel()
        {
            // Default values.
            LatencyThreshold = 75;
            StdDeviationThreshold = 40;
            DefGatewayLatencyThreshold = 95;
            DefGatewayStdDeviationThreshold = 50;
        }


    }
}
