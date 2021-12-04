using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using System.Windows;

namespace OnlineGroceryStore.Models
{
    //class for interaction with database of products
    class ProductService
    {
        string connString;
        SqlConnection connection; //Instance of database connection object
        public ProductService() //Constructor
        {
            try
            {
                //connection string to estabalish connection with database
                connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GroceryStore;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; //Connection String

                //Initializing connection instance and opening the connection
                connection = new SqlConnection(connString); 
                connection.Open(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Function to add new Product to database
        public bool addProduct(Product prod)
        {
            //connection.Open();
            if (connection == null) return false; //checking the connection
            try
            {
                //SQL query to insert new product in the database table
                string query = $"insert into Product(Id, Name, Price, Quantity) values({prod.Id}, '{prod.Name}', {prod.Price}, {prod.Quantity})";

                //Creating and executing SQL statement
                SqlCommand cmd = new SqlCommand(query, connection); 
                if (cmd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        //Function to delete droduct from database by its ID
        public bool deleteProduct(int id)
        {
           // connection.Open();
            if (connection == null) return false; //checking the connection
            try
            {
                //SQL query for deleting product with given id from database
                string query = $"delete from Product where Id={id}";

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

        //function to get product from databse by giving its id
        public Product getProduct(int id)
        {
            //connection.Open();
            if (connection == null) return null;       //checking the database connection
            try
            {
                //SQL query for get a product with given id from database
                string query = $"select * from Product where Id = {id}";

                //Creating and reading data from SQL statement
                SqlCommand cmd = new SqlCommand(query, connection); 
                SqlDataReader dataRdr = cmd.ExecuteReader(); 
                if (dataRdr.Read())   //checking and iterating through the row returned
                {
                    //returning a product object based on the row got from database
                    Product temp = new Product();
                    temp.Id = Convert.ToInt32(dataRdr["Id"]);
                    temp.Name = dataRdr["Name"].ToString();
                    temp.Price = Convert.ToDecimal(dataRdr["Price"]);
                    temp.Quantity = Convert.ToInt32(dataRdr["Quantity"]);
                    //connection.Close();
                    dataRdr.Close();
                    return temp;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        //Function to update quantity of a Product in database whose id is given
        public bool updateProduct(int id, int quan)
        {
            //connection.Open();
            if (connection == null) return false;       //checking the database connection
            try
            {
                //SQL query for updating quantity of a Product from database
                string query = $"update Product set Quantity = Quantity - {quan} where Id={id}";

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

        //function that reads all products in the database and returns a table constituting them
        public DataTable readProducts()
        {

            //connection.Open();
            if (connection == null)     return null;
            try
            {
                //SQQL query to retrieve all records from the Product table
                string query = $"select Id as 'Product ID', Name as 'Product Name', Price, Quantity from Product";

                //Creating and executing SQL statement
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();

                //making a table and filling it based on the records got from databse and returning it
                SqlDataAdapter dadap = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                dadap.Fill(table);
                dadap.Update(table);
                return table;
            }
            catch (Exception ex)
            {
                Console.WriteLine("product service \nread Products" + ex.Message);
                return null;
            }
        }

        //Function to delete the products which have are sold out completely i.e having zero quantity in database
        public bool deleteSoldoutProducts()
        {
            //SQL query to delete all products with zero quantity from database
            string query = $"delete from Product where Quantity = 0";

            //Creating and executing SQL statement
            SqlCommand cmd = new SqlCommand(query, connection); 
            if (cmd.ExecuteNonQuery() > 0)
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
