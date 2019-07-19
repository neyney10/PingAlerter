using PingAlerter.Common;
using PingAlerter.IO.FileSystem;
using PingAlerter.Network;
using PingAlerter.Other.Log;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PingAlerter.Other.MonitorTab
{
    class MonitorService : Observable<MonitorServiceNotify>
    {
        private LatencyMonitor latencyMonitor;

        private int ScanCount = 0;


        public MonitorService(LatencyMonitorConfig configuration)
        {
            latencyMonitor = new LatencyMonitor(configuration);
        }

        public void StartMonitor(IEnumerable<string> addresses)
        {
            latencyMonitor.PreCheck(addresses, // addresses to ping to
                latencyMonitor.Configuration.PreCheckAmountOfSamples, // amount of cycle/tries/iterations to do, more iterations -> more accuracy, but slower.
                latencyMonitor.Configuration.PreCheckAmountOfPingsPerSample, // amount of pings per cycle/iteration, more pings -> more accuracy, but slower.
                30); // amount of time of minimum delay between any two cycles/iterations.

            latencyMonitor.StartMonitor(Latency, 355, 3, 3);
        }


        private void Latency(IReadOnlyDictionary<string, ScanResult> scans, IReadOnlyDictionary<string, ScanHistory> current_history, IReadOnlyDictionary<string, ScanHistory> origin_history)
        {
            int overThreshold = 0;
            foreach (var history in current_history)
            {
                overThreshold += !CheckLatency(history.Value, current_history[NetworkTools.DefaultGatewayAddress], latencyMonitor.Configuration.LatencyThreshold, latencyMonitor.Configuration.StdDeviationThreshold) ? 1 : 0;

                LogData logtext = new LogData(DateTime.Now, "Ping", history.Key, history.Value.Avg.ToString());

                NotifyObservers(new MonitorServiceNotify(logtext, ScanCount,MonitorServiceNotify.Type.Log));
            }


            bool is_router_latency_ok = CheckLatencyToRouter(origin_history, scans[NetworkTools.DefaultGatewayAddress], latencyMonitor.Configuration.DefGatewayLatencyThreshold);
            bool is_latency_stable = CheckStability(current_history[NetworkTools.DefaultGatewayAddress], latencyMonitor.Configuration.DefGatewayStdDeviationThreshold);

            if ((!is_router_latency_ok && !is_latency_stable) || overThreshold > (2.0 / 3.0) * current_history.Count)
            {
                LogData logtext = new LogData(DateTime.Now, "Alert", "Lag Spike", "W.I.P");
                NotifyObservers(new MonitorServiceNotify(logtext, ScanCount, MonitorServiceNotify.Type.OverThreshold));
            }

            ScanCount++;
            Debug.WriteLine("Thresh: " + overThreshold + " | Router lat: " + is_router_latency_ok + " | stable: " + is_latency_stable);
        }

        // default gateway
        private bool CheckLatencyToRouter(IReadOnlyDictionary<string, ScanHistory> HostScanHistoryOrigin, ScanResult result, long threshold)
        {   
            double delta = result.Avg - HostScanHistoryOrigin[NetworkTools.DefaultGatewayAddress].Avg;

            if (delta > threshold)
                return false;

            return true;
        }

        // check last result
        private bool CheckLatency(ScanHistory history, ScanHistory DefaultGatewayHistory, long latency_threshold, long dev_threshold)
        {
            double delta = 0;
            int amount_of_ping_failures = 0;
            foreach (ScanResult scan in history.Results)
            {
                double pure_latency = scan.Avg - DefaultGatewayHistory.Avg;
                delta += pure_latency - (history.Avg - DefaultGatewayHistory.Avg);
                amount_of_ping_failures += scan.Failed-history.Results.First().Failed;
            }

            delta /= history.Results.Count;

            bool is_stable = CheckStability(history, dev_threshold);

            if (delta > latency_threshold || !is_stable || amount_of_ping_failures > 1)
                return false;

            return true;
        }

        // check unstable net
        private bool CheckStability(ScanHistory history, long threshold)
        {
            // ---------------- TEMP -------------------- //
            List<double> samples = new List<double>();

            foreach (ScanResult scan in history.Results)
                samples.Add(scan.Avg);
            // ---------------- TEMP -------------------- //

            double std_deviation = Probability.ProbabilityOP.StandardDeviation(samples);
            Debug.WriteLine("[stabiliy: std_dev: " + std_deviation);
            if (std_deviation > threshold)
                return false;

            return true;
        }

        
    }
}
