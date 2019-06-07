using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingAlerter.Network
{
    public class LatencyMonitorConfig
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

            public int HistorySize { get; set; }

        // Default values.
            public LatencyMonitorConfig() : this(75,40,95,50,3)
            {  }

            public LatencyMonitorConfig(int latencyThreshold, int stdDeviationThreshold, int defGatewayLatencyThreshold, int defGatewayStdDeviationThreshold, int historySize)
            {
         
                LatencyThreshold = latencyThreshold;
                StdDeviationThreshold = stdDeviationThreshold;
                DefGatewayLatencyThreshold = defGatewayLatencyThreshold;
                DefGatewayStdDeviationThreshold = defGatewayStdDeviationThreshold;
                HistorySize = historySize;
            }



    }
}
