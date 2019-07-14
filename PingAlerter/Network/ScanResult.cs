using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace PingAlerter.Network
{
    public class ScanResult
    {
        /// <summary>
        /// List of ping replies in the scan.
        /// </summary>
        public List<PingReply> Replies { get; protected set; }

        /// <summary>
        /// Amount of failed pings (unreachable and such).
        /// </summary>
        public int Failed { get; set; } 

        public long Avg { get { return (Replies.Count - Failed > 0)?  Sum / (Replies.Count-Failed): -1; } }
        public long Max { get; set; }
        public long Min { get; set; }
        private long Sum;

        public ScanResult()
        { this.Replies = new List<PingReply>(); }

        public void AddReply(PingReply pr)
        {
            if (pr.Status != IPStatus.Success)
                Failed++;
            else
            {
               this.Sum += pr.RoundtripTime;
               this.Max = (pr.RoundtripTime > this.Max)? pr.RoundtripTime : this.Max;
               this.Min = (pr.RoundtripTime < this.Max) ? pr.RoundtripTime : this.Max;
            }

            this.Replies.Add(pr);
            
        }
    }
}
