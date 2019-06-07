
using PingAlerter.Other.MonitorTab;
using PingAlerter.ViewModels;
using System.Windows.Controls;


namespace PingAlerter.Views
{
    /// <summary>
    /// Interaction logic for MonitorTab.xaml
    /// </summary>
    public partial class MonitorTabView : UserControl
    {
        public MonitorTabViewModel monitorTabViewModel { get; set; }

        public MonitorTabView()
        {
            InitializeComponent();
           // InitViewModels();
            //BindViewModels();
        }

        private void InitViewModels()
        {
            monitorTabViewModel = new MonitorTabViewModel();
        }

        private void BindViewModels()
        {
            this.DataContext = monitorTabViewModel;
        }


    }
}
