using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingAlerter.Other.MainWindow
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private MainWindowModel Model;

        public MainWindowViewModel(MainWindowModel model)
        {
            this.Model = model;
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


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
        }
    }
}
