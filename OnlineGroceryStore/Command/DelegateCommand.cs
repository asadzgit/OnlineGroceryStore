using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace OnlineGroceryStore.Command
{
    //Class of Delegate Command implementiong ICommand Interface
    class DelegateCommand : ICommand 
    {
        //Built in event of ICommand Interface which occurs when a change occurs in class 
        public event EventHandler CanExecuteChanged //Builtin Event of ICommand Interface occurs when change occur in class 
        {
            add{    CommandManager.RequerySuggested += value;}
            remove{     CommandManager.RequerySuggested -= value;}
        }

        //An instance of Action Delegate which takes object as parameter with void return type
        private Action<object> exe;
        
        //An instance of Predicate Delegate which takes object as parameter with bool return type
        private Predicate<object> canExe;

        //Constructor of Delegate Command
        public DelegateCommand(Predicate<object> canExe, Action<object> exe) 
        {
            this.canExe = canExe;
            this.exe = exe;
        }

        //Overriding CanExecute(object) function of ICommand Interface
        public bool CanExecute(object parameter) 
        {
            return this.canExe(parameter);
        }

        //Overriding Execute(object) function of ICommand Interface
        public void Execute(object parameter) 
        {
            this.exe(parameter);
        }
    }
}
