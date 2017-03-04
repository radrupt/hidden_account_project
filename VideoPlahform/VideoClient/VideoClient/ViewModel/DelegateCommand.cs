using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VideoClient.ViewModel
{
    public class DelegateCommand : ICommand
    {
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;

        public string DisplayName { get; private set; }
        public event EventHandler CanExecuteChanged;
        public bool HasCanExecuted
        {
            get { return _canExecute != null; }
        }

        public DelegateCommand(Action<object> execute, string name)
            : this(execute, null, name)
        { }

        public DelegateCommand(Action<object> execute, Predicate<object> canExecute, string name)
        {
            _execute = execute;
            _canExecute = canExecute;
            DisplayName = name;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }

            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}
