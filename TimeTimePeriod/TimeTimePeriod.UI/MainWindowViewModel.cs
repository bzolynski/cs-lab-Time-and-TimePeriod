using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using TimeTimePeriod.Library;

namespace TimeTimePeriod.UI
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private TimePeriod _second;
        private Time _zegar;
        private TimePeriod _stopwatch;

        private bool _isStopwatchOn;

        public bool IsStopwatchOn
        {
            get { return _isStopwatchOn; }
            set
            {
                _isStopwatchOn = value;
                OnPropertyChanged(nameof(IsStopwatchOn));
            }
        }


        public Time Zegar
        {
            get { return _zegar; }
            set
            {
                _zegar = value;
                OnPropertyChanged(nameof(Zegar));
            }
        }

        public TimePeriod Stopwatch
        {
            get { return _stopwatch; }
            set
            {
                _stopwatch = value;
                OnPropertyChanged(nameof(Stopwatch));
            }
        }

        public ICommand StartStopStopwatchCommand { get; set; }
        public ICommand ResetStopwatchCommand { get; set; }

        public MainWindowViewModel()
        {
            Zegar = new Time(23, 59, 55);
            Stopwatch = new TimePeriod(0);
            _second = new TimePeriod(1);
            ResetStopwatchCommand = new ResetStopwatch(this);
            StartStopStopwatchCommand = new StartStopStopwatch(this);
            StartZegar();


        }

        private void StartZegar()
        {
            var timer = new DispatcherTimer();

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Zegar += _second;
        }
    }
}
