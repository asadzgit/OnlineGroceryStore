using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace OnlineGroceryStore.ViewModels
{
    /*
     * A base class for all the View Models of the project
     * It is implementing INotifyPropertyChanged interface
    */

    class BaseViewModel : INotifyPropertyChanged 
    {
        //Builtin Event INotifyPropertyChanged Interface to notify a change in property
        public event PropertyChangedEventHandler PropertyChanged;

        //Function subscribing the PropertyChanged event
        protected void OnPropertyChanged(string name) 
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
