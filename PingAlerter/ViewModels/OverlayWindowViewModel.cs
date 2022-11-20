using PingAlerter.Common;
using PingAlerter.Common.State;
using PingAlerter.Network;
using PingAlerter.Network.ScanStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PingAlerter.ViewModels
{
    /// <summary>
    /// ViewModel (VM) for OverlayWindow which signals the lag / ping status in terms of colors.
    /// </summary>
    public class OverlayWindowViewModel : BaseViewModel<Object>
    {
        #region Models and hidden fields

        /// <summary>
        /// Describes the status in terms of colors (i.e red colors equals lag).
        /// </summary>
        protected Color statusColor;

        /// <summary>
        /// The amount of windows/control opacity/transparency.
        /// </summary>
        protected float opacity;

        #endregion

        #region Exposing properties to view.

        /// <summary>
        /// Propery of the StatusColor, Describes the status in terms of colors (i.e red colors equals lag).
        /// </summary>
        public Color StatusColor
        {
            get { return this.statusColor; }
            set { this.statusColor = value; OnPropertyChanged("statusColor"); }
        }

        /// <summary>
        /// The amount of windows/control opacity/transparency.
        /// </summary>
        public float Opacity
        {
            get { return this.opacity; }
            set { this.opacity = value; OnPropertyChanged("Opacity"); }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor of the OverlayWindowViewModel with default color of rgb(50,100,150) (bluish) and opacity of 0.5f.
        /// </summary>
        public OverlayWindowViewModel() : this(Color.FromRgb(50,100,150), 0.5f)
        { }

        /// <summary>
        /// Constructor of the OverlayWindowViewModel with dependency injection of starting status color and opacity
        /// <param name="startStatusColor"> The starting color of the status. </paramref>
        /// <param name="opac"> The starting opacity </paramref>
        /// </summary>
        public OverlayWindowViewModel(Color startStatusColor, float opac)
        {
            this.StatusColor = startStatusColor;
            this.Opacity = opac;

            State.Scans.Subscribe(
                new Observer<IScanStorageEvent>(
                    ev => this.OnScanStorageEvent(ev)
                )
            );

        }



        #endregion

        #region TEMP: Latency Status to Color converter

        public Color LatencyStatusToColorConverter(double latency)
        {
            if(latency < 110)
            {
                return Color.FromRgb(50, 150, 70);
            }

            if (latency >= 110)
            {
                return Color.FromRgb(175, 25, 25);
            }

            return Color.FromRgb(0, 0, 0);
        }

        // TEMP - simulation color change.
        private async void SwitchColor(int delayMs)
        {
            await Task.Delay(delayMs);
            //this.StatusColor = LatencyStatusToColorConverter(new Random().Next(0, 150));
            this.StatusColor = Color.FromRgb(50, 150, 70);
 
        }

        private void OnScanStorageEvent(IScanStorageEvent ev)
        {
            if (ev.Type != Network.ScanStorage.Type.add)
                return;

            ICollection<Scan> newScans = ((ScanStorageAddEvent)ev).AdditionalItems;

            foreach (Scan scan in newScans)
            {
                if (scan.Alerts.Count > 0)
                {
                    this.StatusColor = Color.FromRgb(175, 25, 25);
                    this.SwitchColor(1000);
                    return; // switch color only once.
                }
            }
        }

        #endregion
    }
}
