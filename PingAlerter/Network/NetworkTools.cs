using PingAlerter.DataStructures;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PingAlerter.Network
{
    class NetworkTools
    {
       
        private System.Media.SoundPlayer player;

        private Dictionary<string, ScanHistory> HostScanHistory;
        private Dictionary<string, ScanHistory> HostScanHistoryOrigin;
        public static string DefaultGatewayAddress { get; set; }
    

        public NetworkTools()
        {
            if(DefaultGatewayAddress == null)
                DefaultGatewayAddress = GetDefaultGateway().ToString();

            player = new System.Media.SoundPlayer(@"D:\temp\Programs\Fiddler\LoadScript.wav");

            this.HostScanHistory = new Dictionary<string, ScanHistory>();
            this.HostScanHistoryOrigin = new Dictionary<string, ScanHistory>();

          
        
            // PreCheck(new string[] { DefaultGatewayAddress, "8.8.8.8", "31.168.35.130" });     // router ip, google dns, zap.co.il        
        }


        public void PreCheck(IEnumerable<string> hosts, int amount_of_samples, int amount_of_ping_samples,int delay_between_samples)
        {
            Dictionary<string, ScanResult> scans;

            int history_size = 3;
            // create new entries for hosts / clean history.
            foreach (string host in hosts)
            { 
                HostScanHistory.Add(host, new ScanHistory(history_size));
                HostScanHistoryOrigin.Add(host, new ScanHistory(amount_of_samples));
            }


            HostScanHistory.Add(DefaultGatewayAddress, new ScanHistory(history_size));
            HostScanHistoryOrigin.Add(DefaultGatewayAddress, new ScanHistory(amount_of_samples));

            // scan multiple times to get more accurate sample.
            for (int i = 0; i < amount_of_samples; i++)
            {
                Thread.Sleep(delay_between_samples);

                scans = Scan(hosts, amount_of_ping_samples);

                foreach (KeyValuePair<string,ScanResult> scan in scans)
                {
                    HostScanHistoryOrigin[scan.Key].AddResult(scan.Value);

                    Debug.WriteLine("[" + scan.Key + "] Avg rtt: " + scan.Value.Avg);
                }
            }



            // ---------------- TEMP -------------------- //
            foreach (KeyValuePair<string, ScanHistory> history in HostScanHistoryOrigin)
            {
                List<double> samples = new List<double>();
                foreach (ScanResult scan in history.Value.Results)
                    samples.Add(scan.Avg);
                Debug.WriteLine(">> [std dev of " + history.Key + "] " + Probability.ProbabilityOP.StandardDeviation(samples));
            }
            // ---------------- TEMP -------------------- //




        }

        public Dictionary<string, ScanResult> Scan(IEnumerable<string> hosts, int amount_of_samples)
        {
            var current_scans = new Dictionary<string, Task<ScanResult>>();

            var enuHosts = hosts.GetEnumerator();
            while (enuHosts.MoveNext())
            {
                string host = enuHosts.Current;

                var scan_task = Task.Run(() => PingAddress(host, amount_of_samples));
                current_scans.Add(host, scan_task);
            }

            var current_scan_results = new Dictionary<string, ScanResult>();

            foreach(var scan in current_scans)
                current_scan_results.Add(scan.Key, scan.Value.Result);

            return current_scan_results;

        }

        public Thread Monitor(Action<
            IReadOnlyDictionary<string, ScanResult>, 
            IReadOnlyDictionary<string, ScanHistory>, 
            IReadOnlyDictionary<string, ScanHistory>> LatencyFunc,
            int interval,int amount_of_samples, int amount_of_ping_samples) 
        {

            Thread thread = new Thread(() =>
            {
                int max_loop_count = amount_of_ping_samples;
                int loop_count = 0;

                while (true)
                {
                    loop_count = (loop_count + 1) % max_loop_count;
                    Thread.Sleep(interval);
                    var current_scan_results = Scan(HostScanHistoryOrigin.Keys, amount_of_samples);


                    var enuResults = current_scan_results.GetEnumerator();
                    while (enuResults.MoveNext())
                    {
                        KeyValuePair<string, ScanResult> result = enuResults.Current;

                        this.HostScanHistory[result.Key].AddResult(result.Value);

                        Debug.WriteLine("[" + result.Key + "] Avg rtt: " + result.Value.Avg);
                    }


                    if (loop_count == 0)
                        LatencyFunc(current_scan_results, HostScanHistory, HostScanHistoryOrigin);
                }
            });


            thread.Start();

            return thread;
        }

      













        private async Task<ScanResult> PingAddress(string ipaddress,int amount)
        {            
            ScanResult scan_result = new ScanResult();
            for (int i = 0; i < amount; i++)
            {
                try
                {
                    PingReply reply = await Ping(ipaddress);
                    scan_result.AddReply(reply);
                }
                catch (PingException e)
                {
                    // Ignore.

                    Debug.WriteLine("Ping Failed! " + amount);
                }
     
            }

            return scan_result;
        }

        private Task<PingReply> Ping(string ipaddress)
        {
            return new Ping().SendPingAsync(ipaddress, 350);
        }

        public static IPAddress GetDefaultGateway()
        {
            IPAddress result = null;
            var cards = NetworkInterface.GetAllNetworkInterfaces().ToList();
            if (cards.Any())
            {
                foreach (var card in cards)
                {
                    var props = card.GetIPProperties();
                    if (props == null)
                        continue;

                    var gateways = props.GatewayAddresses;
                    if (!gateways.Any())
                        continue;

                    var gateway =
                        gateways.FirstOrDefault(g => g.Address.AddressFamily.ToString() == "InterNetwork");
                    if (gateway == null)
                        continue;

                    result = gateway.Address;
                    break;
                };
            }

            return result;
        }


    }
}
