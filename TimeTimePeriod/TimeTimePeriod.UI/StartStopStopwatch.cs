using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Windows.Threading;

namespace TimeTimePeriod.UI
{
    public class StartStopStopwatch : ICommand
    {
        private readonly MainWindowViewModel _viewModel;

        public event EventHandler CanExecuteChanged;
        private DispatcherTimer _timer;

        public StartStopStopwatch(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;

        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {


            if (!_viewModel.IsStopwatchOn)
            {
                _timer.Start();
                _viewModel.IsStopwatchOn = true;

            }
            else if(_viewModel.IsStopwatchOn)
            {
                _timer.Stop();
                _viewModel.IsStopwatchOn = false;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if(_viewModel.IsStopwatchOn)
                _viewModel.Stopwatch += new Library.TimePeriod(1);
        }
    }
}
