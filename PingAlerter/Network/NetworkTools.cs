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
    public static class NetworkTools
    {
       
        public static string defaultGatewayAddress;
        public static string DefaultGatewayAddress
        { 
            get
            {
                if (defaultGatewayAddress == null)
                    DefaultGatewayAddress = GetDefaultGateway().ToString();

                return defaultGatewayAddress;
            }
            private set
            {
                defaultGatewayAddress = value;
            }

        }

         public static IReadOnlyDictionary<string, ScanResult> Scan(IEnumerable<string> hosts, int amount_of_samples)
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

            foreach (var scan in current_scans)
                current_scan_results.Add(scan.Key, scan.Value.Result);

            return current_scan_results;
        }

        private static async Task<ScanResult> PingAddress(string ipaddress, int amount)
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


        private static Task<PingReply> Ping(string ipaddress)
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
