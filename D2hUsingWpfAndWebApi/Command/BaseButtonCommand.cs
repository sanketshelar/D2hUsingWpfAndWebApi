using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace D2hUsingWpfAndWebApi.Command
{
    class BaseButtonCommand:ICommand
    {
        Action action;

        public BaseButtonCommand(Action action)
        {
            this.action = action;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            action();
        }
    }
}
