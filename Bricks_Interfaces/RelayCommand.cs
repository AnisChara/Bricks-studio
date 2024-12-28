using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bricks_Interfaces
{
    public class RelayCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        Action<object> doAction;
        Func<object, bool> canDoAction;
        public bool CanExecute(object? parameter)
        {
            return canDoAction == null || canDoAction(parameter);
        }

        public RelayCommand(Action<object> doAction,
        Func<object, bool> canDoAction = null)
        {
            this.doAction = doAction;
            this.canDoAction = canDoAction;
        }
        public void Execute(object? parameter)
        {
            this.doAction(parameter);
        }
    }
}