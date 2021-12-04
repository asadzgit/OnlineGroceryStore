using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
namespace OnlineGroceryStore.Models
{
    /*
     * Customer class with data members as Username, Password and Phone number
     * It is implementing INotifyPropertyChanged to signal the UI about change in properties
    */
    class Customer : INotifyPropertyChanged
    {
        //Builtin Event INotifyPropertyChanged Interface to notify a change in property
        public event PropertyChangedEventHandler PropertyChanged;

        //Function subscribing the PropertyChanged event
        private void NotifyPropertyChanged(string name) 
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        //definition of class data members
        private string username; 
        private string password; 
        private string phoneNo;

        //definition of properties for data members which invoke the PropertyChanged event
        public string Username //Property for username
        {
            get { return username; }
            set
            {
                username = value;
                NotifyPropertyChanged("Username"); 
            }
        }

        public string Password //Property for Password
        {
            get { return password; }
            set
            {
                password = value;
                NotifyPropertyChanged("Password"); 
            }
        }

        public string PhoneNo //Property for Phone Number
        {
            get { return phoneNo; }
            set
            {
                phoneNo = value;
                NotifyPropertyChanged("PhoneNo");
            }
        }
    }
}
