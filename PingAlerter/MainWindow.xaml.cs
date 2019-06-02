using PingAlerter.Network;
using PingAlerter.Other.Log;
using PingAlerter.Other.MainWindow;
using PingAlerter.Other.MonitorConfig;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PingAlerter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private System.Media.SoundPlayer player;
        NetworkTools net;
        Thread Monitor;

        // Control models and data sources
        bool StartButtonOn = false;

        // ViewModels //
        MainWindowViewModel mainWindowViewModel; // for general things such as status bar ( should I create a status bar viewModel? )
        MonitorConfigViewModel monitorConfigViewModel; // for Settings tab
        LogViewModel logViewModel; // for Logs tab


        public MainWindow()
        {
            InitializeComponent();
            InitClass();
        }

        public void InitClass()
        {
            player = new System.Media.SoundPlayer(@"D:\temp\Programs\Fiddler\LoadScript.wav");

            // General window
            this.DataContext = this;
            MainWindowModel mainWindowModel = new MainWindowModel();
            mainWindowViewModel = new MainWindowViewModel(mainWindowModel);

            stbar_label_scansvalue.DataContext = mainWindowViewModel;
           // Log tab
           LogModel logModel = new LogModel();
            logViewModel = new LogViewModel(logModel);

            listBox_log.DataContext = logViewModel;

            // Monitor tab


            // Settings tab
            MonitorConfigModel monitorModel = new MonitorConfigModel();
            monitorConfigViewModel= new MonitorConfigViewModel(monitorModel);

            tab_settings.DataContext = monitorConfigViewModel;
        }



        private void btn_start_Click(object sender, RoutedEventArgs e)
        {
            Button btn_start = (Button)sender;

            if(StartButtonOn)
            {
                StartButtonOn = false;
                Monitor.Abort(); // TODO: change it to stop the thread using a boolean or somethin.
                
                btn_start.Background = new LinearGradientBrush(Color.FromRgb(25,200,33),Color.FromRgb(0,233,88),1);
                btn_start.Content = "Start!";

            }
            else
            {
                StartButtonOn = true;
                net = new NetworkTools();

                string[] addresses = new string[listBox_addresses.Items.Count];
                for (int i = 0; i < listBox_addresses.Items.Count; i++)
                {
                    string address = (string)((ListBoxItem)listBox_addresses.Items[i]).Content;

                    addresses[i] = address;
                }


                net.PreCheck(addresses, 5, 4, 30);
                Monitor = net.Monitor(Latency, 450, 5, 3);

                btn_start.Background = new SolidColorBrush(Color.FromRgb(200, 25, 25));
                btn_start.Content = "Stop!";
            }
            
        }

        private void Latency(IReadOnlyDictionary<string, ScanResult> scans, IReadOnlyDictionary<string, ScanHistory> current_history, IReadOnlyDictionary<string, ScanHistory> origin_history)
        {
            int overThreshold = 0;
            foreach (var history in current_history)
            {
                overThreshold += !CheckLatency(history.Value, current_history[NetworkTools.DefaultGatewayAddress], monitorConfigViewModel.LatencyThreshold, monitorConfigViewModel.StdDeviationThreshold) ? 1 : 0;

                mainWindowViewModel.ScanCount++;

                logViewModel.AddLog("Ping " + history.Key + " RTT: " + history.Value.Avg);
            }

            Application.Current.Dispatcher.BeginInvoke((Action)delegate // LATER USE ObservableCollection for this.
            {
                listBox_log.Items.Refresh(); 
            });

            bool is_router_latency_ok = CheckLatencyToRouter(origin_history, scans[NetworkTools.DefaultGatewayAddress], monitorConfigViewModel.DefGatewayLatencyThreshold);
            bool is_latency_stable = CheckStability(current_history[NetworkTools.DefaultGatewayAddress], monitorConfigViewModel.DefGatewayStdDeviationThreshold);

            if ((!is_router_latency_ok && !is_latency_stable) || overThreshold > (2.0 / 3.0) * current_history.Count)
                player.Play();

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
            foreach(ScanResult scan in history.Results)
            {
                double pure_latency = scan.Avg - DefaultGatewayHistory.Avg;
                delta += pure_latency - (history.Avg - DefaultGatewayHistory.Avg);
            }
            delta /= history.Results.Count;

            bool is_stable = CheckStability(history, dev_threshold);

            if (delta > latency_threshold || !is_stable)
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
