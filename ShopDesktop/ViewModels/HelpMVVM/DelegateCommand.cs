using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ShopDesktop.ViewModels.HelpMVVM
{
    class DelegateCommand : ICommand
    {

        Action<object> _action;
        Predicate<object> _canAction;

        public DelegateCommand(Action<object> action, Predicate<object> canAction)
        {
            _action = action;
            _canAction = canAction;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canAction(parameter);
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }

    }
}
