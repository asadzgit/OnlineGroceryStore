using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using OnlineGroceryStore.Command;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows;
using OnlineGroceryStore.Models;
using OnlineGroceryStore.Views;

namespace OnlineGroceryStore.ViewModels
{
    /*
     * providing DataContext to Admin dashboard and Products list Views and provides the functionality to them
     * it inherits Base View Model which implements the INotifyPropertyChanged interface
    */
    class AdminViewModel : BaseViewModel
    {
        //an instance of ICommand which loads new views in the application
        public ICommand UpdateViewCommand { get; set; }

        //the command to add a product in database
        public DelegateCommand addProduct { get; set; }

        //the command to delete a product from database
        public DelegateCommand deleteProduct { get; set; }
        
        //an object of product service class to interact with database
        private ProductService productServiceObj;
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
        //an object of datatable used to deisplay all the available products from the database
        private DataTable viewproducts;
        public DataTable Viewproducts
        {
            get
            {
                return viewproducts;
            }
            set
            {
                viewproducts = value;
                OnPropertyChanged("AvailableProducts");
            }
        }
        public AdminViewModel()     //Constructor
        {
            UpdateViewCommand = new DelegateCommand(canExecute, ViewSelector);
            addProduct = new DelegateCommand(canExecuteAddProduct, executeAddProduct);
            deleteProduct = new DelegateCommand(canExecuteRemoveProduct, removeProduct);
            viewproducts = new DataTable();
            readAllProducts();
        }

        //function to read all the available products from database and show them in products datagrid
        private void readAllProducts()
        {
            try
            {
                productServiceObj = new ProductService();       //Creating product service object to interact with database
                Viewproducts = productServiceObj.readProducts();
                if (Viewproducts == null)
                    MessageBox.Show("Could not read products");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /*
         * it always return true to update the view of the application
        */
        bool canExecute(object o)
        {
            return true;
        }

        //function to  update the view by setting the value of SelectedViewModel based on parameters it receive 
        void ViewSelector(object o)
        {
            if ((o as string).Equals("Products"))
            {
                SelectedViewModel = new ProductViewModel();
            }
            else if ((o as string).Equals("Main"))
            {
                SelectedViewModel = new HomeViewModel();
            }
            else if((o as string).Equals("Admin"))
            {
                SelectedViewModel = new AdminViewModel();
            }
        }

        //function to determine when the user can click Add button
        bool canExecuteAddProduct(object o)
        {
            //Casting command parameters object to object array
            var data = o as object[]; 
            if (data == null)       //checking if no parameter is passed in command. button will be disabled if no parameter
                return false;       
            for (int i = 0; i < 4; i++)     //Checking if any fields is null or not. button will be disabled if no parameter
                if (String.IsNullOrEmpty(data[i].ToString())) 
                    return false;
            
            return true;    //all the fields are filled and user Add button is enabled
        }

        //function to add a product from database
        void executeAddProduct(object o)
        {
            //Casting command parameters object to object array
            var data = o as object[]; 
            try
            {
                if (Convert.ToInt32(data[3]) < 1) //Checking if given quantity is negtaive or zero
                {
                    MessageBox.Show("Quantity must not be negative or zero!"); 
                    return;
                }
                else if (Convert.ToDecimal(data[2]) <= 0) //Checking if given price is negtaive or zero
                {
                    MessageBox.Show("Price must not be negative or zero!"); 
                    return;
                }
                productServiceObj = new ProductService();   //Creating product service object to interact with database
                if (productServiceObj.addProduct(new Product        //creating a new product based on the admin input and inserting in database
                {
                    Id = Convert.ToInt32(data[0]),
                    Name = data[1].ToString(),
                    Price = Convert.ToDecimal(data[2]),
                    Quantity = Convert.ToInt32(data[3])
                }
                )) 
                    MessageBox.Show("Product Successfully Added!"); // Product Added successfully
                else //Showing Product already exist message OR invalid data was filled
                    MessageBox.Show("Either Product already exist with same ID!\nOR\nInvalid name was entered "); 
                productServiceObj.closeDB(); //Closing database
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); //Showing exception message
            }

        }

        //function that determines whether Delete button is enabled or disabled
        bool canExecuteRemoveProduct(object o)
        {
            if (o == null || String.IsNullOrEmpty(o.ToString())) //If ID field is empty, button is disabled
                return false;
            return true;        //button is enabled
        }

        //function to remove a product from database
        void removeProduct(object o)
        {
            var data = o as object[]; //Casting object to object array
            try
            {
                productServiceObj = new ProductService();       //Creating product service object to interact with database
                if (productServiceObj.deleteProduct(Convert.ToInt32(o))) //Deleting product from database and checking status
                    MessageBox.Show("Product Successfully Removed!"); //product removed successfully
                else
                    MessageBox.Show("Product does not exist with same ID!"); //product with given ID does not exist
                productServiceObj.closeDB();    //closing database
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); //Shwoing exception message
            }
        }
    }
}