using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingAlerter.Network
{
    public class LatencyMonitorConfig
    {
        #region Properties
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

        public int PreCheckAmountOfSamples { get; set; }

        public int PreCheckAmountOfPingsPerSample { get; set; }

        #endregion

        // Default values.
        public LatencyMonitorConfig() : this(75, 40, 95, 50, 3,4 ,4 )
        { }

        public LatencyMonitorConfig(int latencyThreshold,
            int stdDeviationThreshold,
            int defGatewayLatencyThreshold,
            int defGatewayStdDeviationThreshold,
            int historySize,
            int PreCheck_AmountOfSamples,
            int PreCheck_AmountOfPingsPerSample)
        {

            this.LatencyThreshold = latencyThreshold;
            this.StdDeviationThreshold = stdDeviationThreshold;
            this.DefGatewayLatencyThreshold = defGatewayLatencyThreshold;
            this.DefGatewayStdDeviationThreshold = defGatewayStdDeviationThreshold;
            this.HistorySize = historySize;
            this.PreCheckAmountOfSamples = PreCheck_AmountOfSamples;
            this.PreCheckAmountOfPingsPerSample = PreCheck_AmountOfPingsPerSample;

        }



    }
}
