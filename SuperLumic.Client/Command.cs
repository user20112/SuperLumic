using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SuperLumic
{
    public class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public bool canExecuteBool = true;
        public bool CanExecuteBool { get { return canExecuteBool; } set { canExecuteBool = value; CanExecute(canExecuteBool); } }
        public EventHandler<object> OnExecute;
        public Command(EventHandler<object> OnExecute)
        {
            this.OnExecute = OnExecute;
        }
        public bool CanExecute(object parameter)
        {
            return CanExecuteBool;
        }

        public void Execute(object parameter)
        {
            OnExecute(this, parameter);
        }
    }
}
