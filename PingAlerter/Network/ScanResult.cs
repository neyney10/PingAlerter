using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace PingAlerter.Network
{
    class ScanResult
    {
        public List<PingReply> Replies { get; protected set; }
        public int Failed { get; set; }
        private long Sum;
        public long Avg { get { return (Replies.Count - Failed > 0)?  Sum / (Replies.Count-Failed): -1; } }

        public ScanResult()
        { this.Replies = new List<PingReply>(); }

        public void AddReply(PingReply pr)
        {
            if (pr.Status != IPStatus.Success) 
                Failed++;
            else this.Sum += pr.RoundtripTime;

            this.Replies.Add(pr);
            
        }
    }
}
