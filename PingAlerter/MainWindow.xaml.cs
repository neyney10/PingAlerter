using PingAlerter.Common;
using PingAlerter.IO;
using PingAlerter.IO.Database;
using PingAlerter.IO.FileSystem;
using PingAlerter.Models;
using PingAlerter.Network;
using PingAlerter.Other.Log;
using PingAlerter.Other.MonitorTab;
using PingAlerter.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PingAlerter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel mainWindowViewModel;

        public MainWindow()
        {
            InitializeComponent();
            InitViewModels();
            BindViewModels();
            //TESTINGTEMP();
        }

        private void BindViewModels()
        {
            this.DataContext = mainWindowViewModel;
        }

        private void InitViewModels()
        {
            mainWindowViewModel = new MainWindowViewModel();

        }

        /// <summary>
        /// /Fore testing porpuses only! TEMP, may delete this method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_StateChanged(object sender, EventArgs e)
        {
            switch (this.WindowState)
            {
                case WindowState.Maximized:
                    // TODO: create sperated method
                    //Hide();
                    break;
                case WindowState.Minimized:
                    //MessageBox.Show("Minimized");
                    break;
                case WindowState.Normal:
                    //MessageBox.Show("Normal");
                    break;
            }
        }

        /// <summary>
        /// for testing porpuses only, TEMP: may delete this function.
        /// </summary>
        private void TESTINGTEMP()
        {
            FileLogger m = new FileLogger(@"D:\LogFile.txt");
            var list = m.ReadLogs();

            ILogger DBLogger = new DBMySQLLogger("Server=127.0.0.1;Database=pingalerterlogs;Uid=root;Pwd=toor;");
            DBLogger.ReadLogs();
        }



    }
}
