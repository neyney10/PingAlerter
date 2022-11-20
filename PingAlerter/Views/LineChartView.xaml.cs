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
using LiveCharts;
using LiveCharts.Wpf;
using PingAlerter.ViewModels;

namespace PingAlerter.Views
{
    /// <summary>
    /// Interaction logic for LineChartView.xaml
    /// </summary>
    public partial class LineChartView : UserControl
    {
        public LineChartViewModel LineChartViewModel
        {
            set { this.DataContext = value; }
            get { return this.DataContext as LineChartViewModel; }
        }

        public LineChartView()
        {
            InitializeComponent();
        }
    }
}
