using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PingAlerter.Network
{
    class LatencyMonitor 
    {
        public LatencyMonitorConfig Configuration { get; set; }

        protected Dictionary<string, ScanHistory> HostScanHistory;
        protected Dictionary<string, ScanHistory> HostScanHistoryOrigin;

        public LatencyMonitor() : this(new LatencyMonitorConfig())
        { }

        public LatencyMonitor(LatencyMonitorConfig configuration)
        {
            this.Configuration = configuration;

            this.HostScanHistoryOrigin = new Dictionary<string, ScanHistory>();
            this.HostScanHistory = new Dictionary<string, ScanHistory>();
        }

        public void PreCheck(IEnumerable<string> hosts, int amount_of_samples, int amount_of_ping_samples, int delay_between_samples)
        {

            // create new entries for hosts / clean history.
            foreach (string host in hosts)
            {
                HostScanHistoryOrigin.Add(host, new ScanHistory(amount_of_samples));
                HostScanHistory.Add(host, new ScanHistory(Configuration.HistorySize));
            }

            HostScanHistoryOrigin.Add(NetworkTools.DefaultGatewayAddress, new ScanHistory(amount_of_samples));
            HostScanHistory.Add(NetworkTools.DefaultGatewayAddress, new ScanHistory(amount_of_samples));


            // scan multiple times to get more accurate sample.
            for (int i = 0; i < amount_of_samples; i++)
            {
                Thread.Sleep(delay_between_samples);

                IReadOnlyDictionary<string, ScanResult> scans = NetworkTools.Scan(hosts, amount_of_ping_samples);

                foreach (KeyValuePair<string, ScanResult> scan in scans)
                {
                    HostScanHistoryOrigin[scan.Key].AddResult(scan.Value);
                    Debug.WriteLine("[" + scan.Key + "] Avg rtt: " + scan.Value.Avg);
                }
            }


        }

        public void StartMonitor(Action<
            IReadOnlyDictionary<string, ScanResult>,
            IReadOnlyDictionary<string, ScanHistory>,
            IReadOnlyDictionary<string, ScanHistory>> LatencyFunc,
            int interval, int amount_of_samples, int amount_of_ping_samples)
        {
            if (HostScanHistoryOrigin.Count == 0)
                return;

            int max_loop_count = amount_of_samples;
            int loop_count = 0;

            while (true)
            {
                loop_count = (loop_count + 1) % max_loop_count;
                Thread.Sleep(interval);
                var current_scan_results = NetworkTools.Scan(HostScanHistoryOrigin.Keys, amount_of_ping_samples);

                StoreResultsInDictionary(current_scan_results);

                if (loop_count == 0)
                    LatencyFunc(current_scan_results, HostScanHistory, HostScanHistoryOrigin);
            }
        }

        private void StoreResultsInDictionary(IReadOnlyDictionary<string,ScanResult> scanResults)
        {
            foreach (KeyValuePair<string, ScanResult> result in scanResults)
            {
                this.HostScanHistory[result.Key].AddResult(result.Value);

                Debug.WriteLine("[" + result.Key + "] Avg rtt: " + result.Value.Avg);
            }
        }

        public void Start()
        {
            throw new NotImplementedException();
        }
    }
}
