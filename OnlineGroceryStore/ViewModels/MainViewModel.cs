using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using OnlineGroceryStore.Command;
using OnlineGroceryStore.Models;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Controls;
using OnlineGroceryStore.Views;

namespace OnlineGroceryStore.ViewModels
{
    /*
     * a class to navigate to Admin and Customer dashboard
     * it inherits Base View Model which implements the INotifyPropertyChanged interface
    */
    class MainViewModel : BaseViewModel
    {
        /*
         * an object of base View Model 
         * it helps to navigate towards other view models
        */
        private BaseViewModel selectedViewModel;
        public BaseViewModel SelectedViewModel
        {
            get { return selectedViewModel; }
            set
            {
                selectedViewModel = value; 
                OnPropertyChanged("SelectedViewModel"); 
            }
        }

        //an instance of ICommand which loads new views in the application
        public ICommand UpdateViewCommand { get; set; }
        public MainViewModel()      //Constructor
        {
            UpdateViewCommand = new DelegateCommand(canExecute, ViewSelector);           
        }

        /*
         * overriding canExecute (object o) of delegate command 
         * it always return true to update the view of the application
        */
        private bool canExecute(object o)
        {
            return true;
        }

        //function to  update the view by setting the value of SelectedViewModel based on parameters it receive 
        private void ViewSelector(object o)
        {

            if ((o as string).Equals("Admin"))
            {
                SelectedViewModel = new AdminViewModel();
            }

            else if ((o as string).Equals("Customer")) 
                SelectedViewModel = new CustomerViewModel();
        }      
    }
}
