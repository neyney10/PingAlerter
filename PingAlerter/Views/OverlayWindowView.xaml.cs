using PingAlerter.Common.State;
using PingAlerter.ViewModels;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace PingAlerter.Views
{
    /// <summary>
    /// Interaction logic for OverlayWindowView.xaml
    /// </summary>
    public partial class OverlayWindowView : Window
    {
        #region ViewModels
        private OverlayWindowViewModel overlayWindowViewModel { get; set; }
        #endregion

        #region Constructor

        public OverlayWindowView()
        {
            InitializeComponent();

            // init ViewModels
            this.overlayWindowViewModel = new OverlayWindowViewModel();
            this.DataContext = this.overlayWindowViewModel;

            // Make the window draggable.
            this.MouseDown += Window_MouseDown;

            // On window close.
            this.Closing += Window_close;

            State.IsOverlayEnabled = true;
        }

        #endregion 

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Window_close(object sender, CancelEventArgs e)
        {
            State.IsOverlayEnabled = false;
        }
    }
}
