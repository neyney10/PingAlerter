using PingAlerter.Common;
using PingAlerter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingAlerter.ViewModels
{
    public class StatusBarViewModel : BaseViewModel<Object>
    {
        private StatusBarModel Model;

        #region Constructor and Init
        public StatusBarViewModel()
        {
            Model = new StatusBarModel();
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
    }
}
