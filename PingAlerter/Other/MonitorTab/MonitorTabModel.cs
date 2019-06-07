using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingAlerter.Other.MonitorTab
{
    class MonitorTabModel
    {
        public ObservableCollection<string> Addresses { get; }
        public string txtb_newAddress { get; set; }

        public MonitorTabModel()
        {
            this.Addresses = new ObservableCollection<string>();

            // Default values
            this.Addresses.Add("8.8.8.8");
            this.Addresses.Add("31.168.35.130"); // zap.co.il
        }




    }
}
