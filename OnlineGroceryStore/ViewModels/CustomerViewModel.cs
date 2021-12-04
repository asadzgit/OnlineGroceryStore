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
using System.Windows.Controls;

namespace OnlineGroceryStore.ViewModels
{   
    /*
     * providing DataContext to customer login/signup page and sale Views and provides the functionality to them
     * it inherits Base View Model which implements the INotifyPropertyChanged interface
    */
    class CustomerViewModel : BaseViewModel
    {
        /*
         * an object of base View Model 
         * it helps to navigate towards other view models
        */
        private BaseViewModel selectedViewModel;
        public BaseViewModel SelectedViewModel
        {
            get { return selectedViewModel; }
            set { selectedViewModel = value; OnPropertyChanged("SelectedViewModel"); }
        }

        //an instance of ICommand which loads new views in the application
        public ICommand UpdateViewCommand { get; set; }

        //the command to login as customer in application,binded with login button
        public DelegateCommand login { get; set; } 

        //the command to sugnup as customer in application,binded with signup button
        public DelegateCommand signup { get; set; } 

        //the command to add a product with some quantity in the cart,binded with add button
        public DelegateCommand addCart { get; set; } //Command for add cart button, add product in cart

        //the command to finish the sale of customer,binded with finish button
        public DelegateCommand chechkout { get; set; } //Command for finish button, performing checkout

        //an object of customer service class to interact with database
        private CustomerService cs;

        //an object of product service class to interact with database
        private ProductService pr;

        //Instance of DataSet used for cart datagrid's data
        private DataSet dataSet; 

        //Instance of DataTable used for cart datagrid's data
        private DataTable dataTable;

        //List used to product available in cart
        private List<Product> cart;

        //the datatable binded with cart datagrid of customer sale, to show the items going to be purchased 
        private DataTable cartproducts;
        public DataTable Cartproducts
        {
            get
            {
                return cartproducts;
            }
            set
            {
                cartproducts = value;
                OnPropertyChanged("CartProducts");
            }
        }

        //the datatable binded with available items' datagrid of customer sale, to show all the available items in database
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
                OnPropertyChanged("viewproducts");
            }
        }
        public CustomerViewModel()      //Constructor
        {
            UpdateViewCommand = new DelegateCommand(canExecute, ViewSelector);
            login = new DelegateCommand(canExecuteLogin, ExecuteLogin); //Initializing Delegate Command with two function with one parameter of object type and one with bool return type and other one is with void respectively
            signup = new DelegateCommand(canExecuteSignup, ExecuteSignup); //Initializing Delegate Command with two function with one parameter of object type and one with bool return type and other one is with void respectively
            addCart = new DelegateCommand(canExecuteAddCart, ExecuteAddCart); //Initializing Delegate Command with two function with one parameter of object type and one with bool return type and other one is with void respectively
            chechkout = new DelegateCommand(canExecuteCheckOut, ExecuteCheckOut); //Initializing Delegate Command with two function with one parameter of object type and one with bool return type and other one is with void respectively
            Viewproducts = new DataTable();
            Cartproducts = new DataTable();
            dataTable = new DataTable();
            readAllProducts();
            dataSet = new DataSet(); //Creating DataSet Object
            dataTable = new DataTable(); //Creating DataTable Object
            dataSet.Tables.Add(dataTable);
            dataTable.Columns.Add("Product ID"); //Adding Column Product ID in dataTable 
            dataTable.Columns.Add("Product Name"); //Adding Column Product Name in dataTable
            dataTable.Columns.Add("Price"); //Adding Column Price in dataTable
            dataTable.Columns.Add("Quantity"); //Adding Column Quantity in dataTable
            cart = new List<Product>();
        }

        /*
         * it always return true to update the view of the application
        */
        bool canExecute(object o)
        {
            return true;
        }

        //function to read all the available products from database and show in available products datagrid
        private void readAllProducts()
        {
            try
            {
                ProductService pr = new ProductService();
                Viewproducts = pr.readProducts();
                
                if (Viewproducts == null)
                    MessageBox.Show("Could not read products");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.Source);
            }
        }

        //function to  update the view by setting the value of SelectedViewModel based on parameters it receive 
        void ViewSelector(object o)
        {
            if ((o as string).Equals("Sale"))
            {
                SelectedViewModel = new SaleViewModel();
            }
            else if ((o as string).Equals("Back"))
            {
                SelectedViewModel = new HomeViewModel();
            }
            else if ((o as string).Equals("logout"))
            {                          
                SelectedViewModel = new CustomerViewModel();
            }
        }

        //function to determine whether login button is enabled or not
        bool canExecuteLogin(object o)
        {
            var data = o as object[]; //Casting object to object array
            if (data == null) return false;     //if no data passed, disable the button
            if (String.IsNullOrEmpty(data[0].ToString()) || String.IsNullOrEmpty(((PasswordBox)data[1]).Password)) //if any field is empty, disable the button
                return false;
            return true;        //button is enabled
        }

        //function to login, validate customer from database and moved it to Sale Screen
        void ExecuteLogin(object o)
        {
            var data = o as object[]; //Casting object to object array
            try
            {
                cs = new CustomerService(); //Creating Customer service object to interact with database

                //validating customer from database
                if (cs.verifyCustomer(data[0].ToString(), ((PasswordBox)data[1]).Password)) 
                    ViewSelector("Sale"); //navigating to Customer Sale screen
                else
                    MessageBox.Show("Invalid Credentials!"); //Showing Invalid Credentials message
                cs.closeDB(); //Closing database
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); //Showing exception message
            }
        }

        //function to determine whether signUP button is enabled or not
        bool canExecuteSignup(object o)
        {
            var data = o as object[]; //Casting object to object array
            if (data == null) return false;     //no data found, so button is disabled

            //if any field empty, disable the button
            if (String.IsNullOrEmpty(data[0].ToString()) || String.IsNullOrEmpty(((PasswordBox)data[1]).Password) || String.IsNullOrEmpty(data[2].ToString())) 
                return false;
            return true;
        }

        //function to signup , add customer in database
        void ExecuteSignup(object o)
        {
            var data = o as object[]; //Casting object to object array
            string phone = ((TextBox)data[2]).Text;

            /*
             * validating the phone number
             * if it starts with 03 and has 11 digits OR statrts with 923 and have 12 digits, only then accept it
            */
            if ((phone.IndexOf("03") == 0 && phone.Length == 11) || (phone.IndexOf("923") == 0 && phone.Length == 12)) 
            {
                for (int i = 2; i < phone.Length; i++)
                    if (phone[i] < '0' || phone[i] > '9') 
                    {
                        MessageBox.Show("Invalid Phone Number!"); //Showing Invalid Phone Number message
                        return;
                    }
            }
            else
            {
                MessageBox.Show("Invalid Phone Number!"); //Showing Invalid Phone Number message
                return;
            }
            try
            {
                //Creating Customer service object to interact with database
                cs = new CustomerService(); 
                if (cs.signUP(              //adding the customer record in database
                    new Customer
                    {
                        Username = ((TextBox)data[0]).Text,
                        Password = ((PasswordBox)data[1]).Password,
                        PhoneNo = ((TextBox)data[2]).Text
                    }
                )) 
                {
                    //customer record added successfully
                    MessageBox.Show("Successfully Signed Up!");
                    var temp = o as object[]; //Casting object to object array
                    /*
                     * making the signup form fields empty
                    */
                    (temp[0] as TextBox).Text= "";      
                    (temp[1] as PasswordBox).Password = "";
                    (temp[2] as TextBox).Text = "";
                }
                else    //could not add the customer, customer already exist message
                    MessageBox.Show("Customer already exist!");
                cs.closeDB(); //Closing database
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); //Showing exception message
            }

        }
        
        bool canExecuteAddCart(object o)
        {
            var data = o as object[]; //Casting object to object array
            if (data == null) return false;
            for (int i = 0; i < 2; i++)
                if (String.IsNullOrEmpty(data[i].ToString())) //Checking any field is empty or not
                    return false;
            return true;
        }
        void ExecuteAddCart(object o)
        {
            var data = o as object[]; //Casting object to object array
            try
            {
                    int id = Convert.ToInt32(data[0]);
                    int quan = Convert.ToInt32(data[1]);
                
                if (quan < 1) //Checking is given quantity negative/zero or not
                {
                    //Showing negative or zero quanity message
                    MessageBox.Show("Quantity must not negative or zero!"); 
                    return;
                }

                //Creating product service object to interact with database
                pr = new ProductService(); 
                Product prod = pr.getProduct(id); //Retriving product from cart
                if (prod == null) //Validating product exist or not
                {
                    MessageBox.Show("Product does not exist!"); //Showing product already exist message
                    return;
                }
                int crtQuan = getQuantity(id); //Retriving product quantity from cart
                
                //Validating quatity
                if (crtQuan == -1 && prod.Quantity < quan || prod.Quantity  < quan) 
                {
                    //showing message if no more quantity is left
                    if(prod.Quantity == 0)
                    {
                        MessageBox.Show("Sorry, No more items of this product are left");
                        return;
                    }
                    //Showing low quantity message    
                    MessageBox.Show($"Sorry, Only { prod.Quantity } Products are available!");
                    return;
                }

                //Changing product quanity
                prod.Quantity = quan; 
                increaseQuantity(prod); //Adding product in cart list if it not exist already,otherwise increase quantity add new product
                var row = dataTable.NewRow(); //New row object in dataTable
                row["Product ID"] = prod.Id; //Adding Product ID value in dataTable's new row
                row["Product Name"] = prod.Name; //Adding Product Name value in dataTable's new row
                row["Price"] = prod.Price; //Adding Price value in dataTable's new row
                row["Quantity"] = prod.Quantity; //Adding Quantity value in dataTable's new row
                dataTable.Rows.Add(row); //Adding row in dataTable
                Cartproducts = dataTable;  //updating cart
                Viewproducts = new DataTable();  //clearaing available products datagrid
                
                if(!pr.updateProduct(id, quan))     MessageBox.Show("Could not update product"); //updating the quantity in database
                readAllProducts();      //updating the available products in datagrid

                pr.closeDB(); //Closing database
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message ); //Showing exception message
            }
        }
        bool canExecuteCheckOut(object o)
        {
            if (cart.Count > 0) //Checking count of products in cart List
                return true;
            return false;
        }
        void ExecuteCheckOut(object o)
        {
            try
            {
                int purchasedTill = cart.Count;
                pr = new ProductService(); //Creating product service object to interact with database
                Viewproducts = pr.readProducts();  //updating the available product datagrid
                if (Viewproducts == null)
                    MessageBox.Show("Could not read products");
                string receipt = $"{new String('-', 20)} Receipt {new String('-', 20)}\n"; //generating receipt
                decimal total = 0M; //variable holding total anount ti be paid
                receipt += $"{String.Format("{0,-20:D}", "Product")} {String.Format("{0,-20:D}","Unit Price")} {String.Format("{0,-10:D}","Quantity" )}\n"; //Adding products in receipt
                receipt += new String('-', 50);
                receipt += '\n';
                for (int i = 0; i < cart.Count; i++)
                {
                    receipt += $"{String.Format("{0,-20:D}", cart[i].Name.ToString())} {String.Format("{0,-20:D}", cart[i].Price.ToString())}      {String.Format("{0,-10:D}", cart[i].Quantity.ToString())}\n"; //Adding products in receipt
                    total += cart[i].Price * cart[i].Quantity; //Calculating net total
                }
                receipt += new String('-', 50); 
                receipt += $"\nNet Total = {total}"; //showing total amount in reseipt
                MessageBox.Show(receipt); //Showing receipt
                
                //Deleting products with zero quantity
                pr.deleteSoldoutProducts(); 
                pr.closeDB(); //Closing database
                cart.Clear(); //Clearing products List
                SelectedViewModel = new CustomerViewModel();
                Cartproducts = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); //Showing exception message
            }
        }

        //function to add product or increase product quantity in cart list if that product is already there
        private void increaseQuantity(Product prod) 
        {
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Id == prod.Id)
                {
                    cart[i].Quantity += prod.Quantity;  //Increasing product quantity in cart
                    return;
                }
            cart.Add(prod);  //Adding new product in cart
        }

        //function to retrieve quantity of a product from cart list
        private int getQuantity(int id) 
        {
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Id == id)
                    return cart[i].Quantity;
            return -1; //If product does not exist in cart list
        }
    }
}
