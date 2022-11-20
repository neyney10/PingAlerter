using PingAlerter.Common;
using PingAlerter.Common.State;
using PingAlerter.Models;
using PingAlerter.Network;
using PingAlerter.Network.ScanStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PingAlerter.ViewModels
{
    public class StatusBarViewModel : BaseViewModel<Object>
    {
        private StatusBarModel Model;

        #region Constructor and Init
        public StatusBarViewModel()
        {
            Model = new StatusBarModel();

            State.Scans.Subscribe(
                new Observer<IScanStorageEvent>(
                    ev => this.OnScanStorageEvent(ev)
                )
            );
        }

        public int ScanCount
        {
            get { return Model.ScanCount; }
            set
            {
                Model.ScanCount = value;
                OnPropertyChanged("ScanCount");
            }
        }

        #endregion

        private void OnScanStorageEvent(IScanStorageEvent ev)
        {
            switch (ev.Type)
            {
                case Network.ScanStorage.Type.add:
                    this.ScanCount = ev.Storage.Scans.Count();
                    break;
            }
        }

        #region TEMP - EXPERIMENTAL VIEW MODEL

        public ExperimentalViewModel experimentalViewModel { get; } = new ExperimentalViewModel();

        public class ExperimentalViewModel
        {

            public ICommand Btn_experimental_OnClick { get; set; }

            public ExperimentalViewModel()
            {
                this.Btn_experimental_OnClick = new ExperimentalCommand();
            }

            public class ExperimentalCommand : ICommand
            {
                public event EventHandler CanExecuteChanged;
                private Views.OverlayWindowView overlayWindow;

                public bool CanExecute(object parameter)
                {
                    return true;
                }

                public void Execute(object parameter)
                {
                    if (!State.IsOverlayEnabled)
                    {
                        this.overlayWindow =  new Views.OverlayWindowView();
                        this.overlayWindow.Show();
                    }
                    else this.overlayWindow.Close();
                }
            }
        }
        #endregion
    }
}
