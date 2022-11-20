using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace PingAlerter.Network
{
    public class Scan
    {
        public DateTime Timestamp { get; }
        public IReadOnlyCollection<string> ScannedHosts { get; }
        public IReadOnlyDictionary<string, ScanResult> Results { get; }
        public LatencyMonitorConfig Configuration { get; }
        public IReadOnlyCollection<ScanAlert> Alerts;

        public Scan(
            DateTime timestamp,
            LatencyMonitorConfig config,
            IReadOnlyDictionary<string, ScanResult> results,
            IReadOnlyCollection<ScanAlert> alerts)
        {
            this.Timestamp = timestamp;
            this.ScannedHosts = results.Keys.ToList();
            this.Results = results;
            this.Configuration = config;
            this.Alerts = alerts;
        }


        public ScanLog ToLog()
        {
            return new ScanLog(this.Timestamp, this.Results, this.Alerts);
        }

    }
}
