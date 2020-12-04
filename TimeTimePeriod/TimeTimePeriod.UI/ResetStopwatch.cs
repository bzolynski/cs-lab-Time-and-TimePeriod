using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TimeTimePeriod.Library;

namespace TimeTimePeriod.UI
{
    public class ResetStopwatch : ICommand
    {
        private readonly MainWindowViewModel _viewModel;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public ResetStopwatch(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        public bool CanExecute(object parameter)
        {
            return !_viewModel.IsStopwatchOn;
        }

        public void Execute(object parameter)
        {
            _viewModel.Stopwatch = new TimePeriod(0);
        }
    }
}
