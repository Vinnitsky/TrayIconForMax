using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Gelin.S4B.WPFHost.MVVM
{
    public sealed class RelayCommand : ICommand
    {
        private Action<object> _execute;
        private Func<object, Task> _executeAsync;

        private Predicate<object> _canExecute;

        private event EventHandler CanExecuteChangedInternal;

        public RelayCommand(Action<object> execute)
            : this(execute, DefaultCanExecute)
        {
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException("execute");
            _canExecute = canExecute ?? throw new ArgumentNullException("canExecute");
        }
        public RelayCommand(Func<object, Task> execute, Predicate<object> canExecute, bool isAsync)
        {
            _executeAsync = execute ?? throw new ArgumentNullException("execute");
            _canExecute = canExecute ?? throw new ArgumentNullException("canExecute");
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                //CommandManager.RequerySuggested += value;
                CanExecuteChangedInternal += value;
            }

            remove
            {
                //CommandManager.RequerySuggested -= value;
                CanExecuteChangedInternal -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute != null && _canExecute(parameter);
        }

        public async void Execute(object parameter)
        {
            if (_execute != null)
                _execute(parameter);
            else
            {
                if (_executeAsync != null)
                    await _executeAsync(parameter);
            }
        }

        public void OnCanExecuteChanged()
        {
            EventHandler handler = this.CanExecuteChangedInternal;
            if (handler != null)
            {
                //DispatcherHelper.BeginInvokeOnUIThread(() => handler.Invoke(this, EventArgs.Empty));
                handler.Invoke(this, EventArgs.Empty);
            }
        }

        public void Destroy()
        {
            _canExecute = _ => false;
            _execute = _ => { return; };
        }

        private static bool DefaultCanExecute(object parameter)
        {
            return true;
        }
    }
}
