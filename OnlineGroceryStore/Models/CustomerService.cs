using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using System.Windows;
namespace OnlineGroceryStore.Models
{
    //class for interaction with database of customers
    class CustomerService
    {
        SqlConnection connection; //Instance of database connection object
        public CustomerService()
        {
            try
            {
                //connection string to estabalish connection with database
                string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GroceryStore;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

                //Initializing connection instance and opening the connection
                connection = new SqlConnection(connString);
                connection.Open();
            }
            catch (Exception ex)//handling exception
            {
                MessageBox.Show(ex.Message);
            }
        }

        //function to add a new customer record in database
        public bool signUP(Customer custom) 
        {
            //con.Open();
            if (connection == null) return false;      //checking the connection
            try
            {
                //sql query to insert new user record in database
                string query = $"insert into Customer(UserName, Password, Phoneno) values('{custom.Username}', '{custom.Password}', '{custom.PhoneNo}')"; //SQL query for inserting new customer in database

                //Creating and executing SQL statement
                SqlCommand cmd = new SqlCommand(query, connection); 
                if (cmd.ExecuteNonQuery() > 0)               
                    return true;
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        //Function to validating the usernmae and password entered by the user from database
        public bool verifyCustomer(string username, string password) 
        {
         
            if (connection == null) return false;      //checking the database connection
            
            //SQL parameterized query to get rows with required username and password
            string query = $"select * from Customer where UserName = @user and Password = @pass";

            //defining parameters for parameterized query
            SqlParameter user = new SqlParameter("user", username); 
            SqlParameter pass = new SqlParameter("pass", password);

            //Creating SQL statement
            SqlCommand cmd = new SqlCommand(query, connection);

            //initializing parameters for parameterized query
            cmd.Parameters.Add(user);       
            cmd.Parameters.Add(pass);       
            
            SqlDataReader dr = cmd.ExecuteReader();     //Reading data 
            if (dr.HasRows)                             //Checking if datareader contain any row
                return true;
            return false;
        }

        //Function to close the connection with database
        public void closeDB() 
        {
            connection.Close(); 
        }
    }
}
