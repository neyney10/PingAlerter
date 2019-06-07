using PingAlerter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PingAlerter.Views
{
    /// <summary>
    /// Interaction logic for LogsTabView.xaml
    /// </summary>
    public partial class LogsTabView : UserControl
    {
        public LogViewModel logViewModel { get; set; }

        public LogsTabView()
        {
            InitializeComponent();
           // InitViewModels();
           // BindViewModels();
        }

        private void InitViewModels()
        {
            logViewModel = new LogViewModel();
        }

        private void BindViewModels()
        {
            this.DataContext = logViewModel;
        }
    }
}
