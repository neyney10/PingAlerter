using PingAlerter.Common;
using System;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Media;
using PingAlerter.Network;
using System.Windows;
using System.Collections.Generic;
using LiveCharts.Defaults;
using PingAlerter.Common.State;
using PingAlerter.Network.ScanStorage;

namespace PingAlerter.ViewModels
{
    public class LineChartViewModel : BaseViewModel<Object>
    {
        public SeriesCollection SeriesCollection { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public double MaxValue { get; set; }
        private Dictionary<string, LineSeries> addressScansSerieses = new Dictionary<string, LineSeries>();

        #region Constructor and Init
        public LineChartViewModel()
        {
            this.MaxValue = 150;

            SeriesCollection = new SeriesCollection();

            YFormatter = value => value.ToString("C");
            
            // temp
            Observer<IScanStorageEvent> o = new Observer<IScanStorageEvent>(
                (ev) =>
                {
                    Application.Current.Dispatcher.BeginInvoke((Action)delegate 
                    {
                        switch (ev.Type)
                        {
                            case Network.ScanStorage.Type.add:
                                ICollection<Scan> additionalItems = ((ScanStorageAddEvent)ev).AdditionalItems;
                                foreach (Scan scan in additionalItems)
                                {
                                    foreach (var entry in scan.Results)
                                    {
                                        string ipAddress = entry.Key;
                                        if (ipAddress == "0.0.0.0")
                                            continue;

                                        ScanResult scanResult = entry.Value;

                                        this.MaxValue = Math.Max(this.MaxValue, (double)scanResult.Avg);

                                        if (!this.addressScansSerieses.ContainsKey(ipAddress))
                                        {
                                            LineSeries newSeriesForThisAddress = new LineSeries { Title = ipAddress, Values = new ChartValues<ObservableValue> { } };
                                            this.addressScansSerieses.Add(ipAddress, newSeriesForThisAddress);
                                            this.SeriesCollection.Add(newSeriesForThisAddress);
                                        }

                                        LineSeries series = this.addressScansSerieses[ipAddress];
                                        series.Values.Add(new ObservableValue((Int32)scanResult.Avg));
                                        if (series.Values.Count > 25)
                                            series.Values.RemoveAt(0);
                                    }
                                }
                               
                                break;
                        }
  
                    }); 
                }
            );
            State.Scans.Subscribe(o);
        }
   
        #endregion
    }
}
