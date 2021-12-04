using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
namespace OnlineGroceryStore.Models
{
    /*
     * Product class with data members as id, name, price and quantity
     * It is implementing INotifyPropertyChanged to signal the UI about change in properties
    */
    class Product : INotifyPropertyChanged 
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
        private int id; 
        private string name; 
        private decimal price; 
        private int quantity;

        //definition of properties for data members which invoke the PropertyChanged event
        public int Id //Property for ID
        {
            get { return id; }
            set
            {
                id = value;
                NotifyPropertyChanged("Id"); 
            }
        }

        public string Name //Property for Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChanged("Name");
            }
        }

        public decimal Price //Property for Price
        {
            get { return price; }
            set
            {
                price = value;
                NotifyPropertyChanged("Price");
            }
        }

        public int Quantity //Property for Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                NotifyPropertyChanged("Quantity");
            }
        }
    }
}
