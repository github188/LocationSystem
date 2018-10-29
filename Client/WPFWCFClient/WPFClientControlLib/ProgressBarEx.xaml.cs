﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace WPFClientControlLib
{
    /// <summary>
    /// Interaction logic for ProgressBarEx.xaml
    /// </summary>
    public partial class ProgressBarEx : UserControl
    {
        public ProgressBarEx()
        {
            InitializeComponent();
        }

        public Stopwatch Stopwatch=new Stopwatch();

        public DispatcherTimer timer;

        public double Value
        {
            get { return ProgressBar1.Value; }
            set
            {
                ProgressBar1.Value = value;
                if (IsBusy==false)
                {
                    Start();
                }
            }
        }

        public bool IsBusy { get; set; }

        public void Start()
        {
            IsBusy = true;
            if (timer == null)
            {
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(100);
                timer.Tick += Timer_Tick;
                timer.Start();
            }
            Stopwatch.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            LbTimer.Content = Stopwatch.Elapsed.ToString();
        }

        public void Stop()
        {
            IsBusy = false;
            Stopwatch.Stop();
            timer.Stop();
            timer = null;
        }
    }
}