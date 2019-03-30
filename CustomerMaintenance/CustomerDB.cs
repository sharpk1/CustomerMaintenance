//AUTHOR:	Kush Shah, David Daniel, Jennifer Ellefson
//COURSE:		ISYS 315:501  (e.g., ISYS 
//			250.501)
//FORM:		CustomerDB.cs (e.g., frmHowManyYears.cs)
//PURPOSE:		The purpose of this CustomerDB is to compile 
//			the queries and naming that pertain to each of the
//			columns that is connected to our database in MySQL.
//			This includes methods of adding, updating, and modifying customers.
//			
//			
//INITIALIZE:	The form will load the customer information 
//			based on the data imported from the MySQL connection.
//			
//			
//INPUT:		The user will input anything that has to do with  
//			the customer information.
//			
//			
//			
//PROCESS:		To modify the customer, the information has to be changed
//			to add a customer, the information must be inputted. 
//			
//OUTPUT:		The output of the program will be displaying the customer
//			information and having the changes reflect in MySQL.
//			
//			
//TERMINATE:	This program does not require any cleaning up after 
//			its execution of its purpose. 
//			
//			
//HONOR CODE:	“On my honor, as an Aggie, I have neither given 
//			nor received unauthorized aid on this academic 
//			work.”

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace CustomerMaintenance
{
    public static class CustomerDB
    {
        /// <summary>
        /// This method pulls information from MySQL into Visual Studio.
        /// </summary>
        /// <param>@Customer_ID</param>
        /// <returns></returns>
        public static Customer GetCustomer(int customerID)
        {
            //This code stipulates which information gets pulled from MySQL and from where it is getting pulled. 
            MySqlConnection connection = Wildcat.GetConnection();
            string strSelectStatement
                = "SELECT CUSTOMER_ID, CUSTOMER_FIRST_NAME, CUSTOMER_LAST_NAME, CUSTOMER_STREET_NUMBER, CUSTOMER_STREET_NAME, CUSTOMER_CITY, CUSTOMER_STATE, CUSTOMER_PHONE, CUSTOMER_REFERRED_BY "
                + "FROM customer "
                + " WHERE CUSTOMER_ID = @CUSTOMER_ID";
            MySqlCommand selectCommand =
                new MySqlCommand(strSelectStatement, connection);
            selectCommand.Parameters.AddWithValue("@CUSTOMER_ID", customerID);

            try
            {
                //This opens the connection, and converts the information being pulled from MySQL into a string. This string then goes into a textbox we have set.
                connection.Open();
                MySqlDataReader custReader =
                    selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (custReader.Read())
                {
                    Customer customer = new Customer();
                    customer.CUSTOMER_ID = (int)custReader["CUSTOMER_ID"];
                    customer.CUSTOMER_FIRST_NAME = custReader["CUSTOMER_FIRST_NAME"].ToString();
                    customer.CUSTOMER_LAST_NAME = custReader["CUSTOMER_LAST_NAME"].ToString();
                    customer.CUSTOMER_STREET_NUMBER = custReader["CUSTOMER_STREET_NUMBER"].ToString();
                    customer.CUSTOMER_STREET_NAME = custReader["CUSTOMER_STREET_NAME"].ToString();
                    customer.CUSTOMER_CITY = custReader["CUSTOMER_CITY"].ToString();
                    customer.CUSTOMER_STATE = custReader["CUSTOMER_STATE"].ToString();
                    customer.CUSTOMER_PHONE = custReader["CUSTOMER_PHONE"].ToString();
                    customer.CUSTOMER_REFERRED_BY = custReader["CUSTOMER_REFERRED_BY"].ToString();
                    return customer;
                }
                //If it pulls no information, it returns null.
                else
                {
                    return null;
                }
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
        /// <summary>
        /// This places the customer called upon into a list.
        /// </summary>
        /// <param>@CUSTOMER_FIRST_NAME, @CUSTOMER_LAST_NAME</param>
        /// <returns></returns>
        public static List<Customer> GetCustomerByName (string CUSTOMER_FIRST_NAME, string CUSTOMER_LAST_NAME)
        {
            //Using the SQL query to get the headers from the database that is customer.
            MySqlConnection connection = Wildcat.GetConnection();
            string selectStatement
                = "Select CUSTOMER_ID, CUSTOMER_LAST_NAME, CUSTOMER_FIRST_NAME, CUSTOMER_STREET_NAME, CUSTOMER_STREET_NUMBER, CUSTOMER_CITY, CUSTOMER_STATE, CUSTOMER_PHONE"
                + "From customer"
                + "Where";

            //If a null value is entered this will inform the user.
            if(!(string.IsNullOrEmpty(CUSTOMER_FIRST_NAME)) && !(string.IsNullOrEmpty(CUSTOMER_LAST_NAME)))
            {
                selectStatement = selectStatement
                    + "CUSTOMER_FIRST_NAME = @CUSTOMER_FIRST_NAME"
                    + "AND CUSTOMER_LAST_NAME = @CUSTOMER_LAST_NAME";
            }
            else if (!string.IsNullOrEmpty(CUSTOMER_FIRST_NAME))
            {
                selectStatement = selectStatement
                    + "CUSTOMER_FIRST_NAME = @CUSTOMER_FIRST_NAME";
            }
            else if (!string.IsNullOrEmpty(CUSTOMER_LAST_NAME))
            {
                selectStatement = selectStatement
                    + "CUSTOMER_LAST_NAME = @CUSTOMER_LAST_NAME";
            }

            MySqlCommand selectCommand =
                new MySqlCommand(selectStatement, connection);

            selectCommand.Parameters.AddWithValue("@CUSTOMER_FIRST_NAME", CUSTOMER_FIRST_NAME);
            selectCommand.Parameters.AddWithValue("@CUSTOMER_LAST_NAME", CUSTOMER_LAST_NAME);

            try
            {
                //This opens the connection.
                connection.Open();
                MySqlDataReader custReader =
                    selectCommand.ExecuteReader();

                List<Customer> customersList = new List<Customer>();

                while(custReader.Read())
                {
                    //This generates the customer list.
                    Customer customer = new Customer();
                    customer.CUSTOMER_ID = (int)custReader["CUSTOMER_ID"];
                    customer.CUSTOMER_FIRST_NAME = custReader["CUSTOMER_FIRST_NAME"].ToString();
                    customer.CUSTOMER_LAST_NAME = custReader["CUSTOMER_LAST_NAME"].ToString();
                    customer.CUSTOMER_STREET_NAME = custReader["CUSTOMER_STREET_NAME"].ToString();
                    customer.CUSTOMER_STREET_NUMBER = custReader["CUSTOMER_STREET_NUMBER"].ToString();
                    customer.CUSTOMER_CITY = custReader["CUSTOMER_CITY"].ToString();
                    customer.CUSTOMER_STATE = custReader["CUSTOMER_STATE"].ToString();
                    customer.CUSTOMER_PHONE = custReader["CUSTOMER_PHONE"].ToString();
                    customer.CUSTOMER_REFERRED_BY = custReader["CUSTOMER_REFERRED_BY"].ToString();
                    customersList.Add(customer);

                }

                if(customersList.Count() >0)
                {
                    return customersList;
                }
                else
                {
                    return null;
                }
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// This code adds the new customer information to MySQL, and stipulates which fields it will be populating.
        /// </summary>
        /// <param>@CUSTOMER_ID, @CUSTOMER_FIRST_NAME, @CUSTOMER_LAST_NAME, @CUSTOMER_STREET_NAME, @CUSTOMER_STREET_NUMBER, @CUSTOMER_CITY, @CUSTOMER_STATE, @CUSTOMER_PHONE, @CUSTOMER_REFERRED_BY</param>
        /// <returns></returns>
        public static int AddCustomer(Customer customer)
        {
            //This is the logic used to add a customer.
            MySqlConnection connection = Wildcat.GetConnection();
            int newCUSTOMER_ID = GetMaxCustomer_ID() + 1;
            string strInsertStatement =
                "INSERT INTO customer " +
                "(CUSTOMER_ID, CUSTOMER_FIRST_NAME, CUSTOMER_LAST_NAME, CUSTOMER_STREET_NAME, CUSTOMER_STREET_NUMBER, CUSTOMER_CITY, CUSTOMER_STATE, CUSTOMER_PHONE, CUSTOMER_REFERRED_BY) " +
                "VALUES (@CUSTOMER_ID, @CUSTOMER_FIRST_NAME, @CUSTOMER_LAST_NAME, @CUSTOMER_STREET_NAME, @CUSTOMER_STREET_NUMBER, @CUSTOMER_CITY, @CUSTOMER_STATE, @CUSTOMER_PHONE, @CUSTOMER_REFERRED_BY)";
            MySqlCommand insertCommand =
                new MySqlCommand(strInsertStatement, connection);
            insertCommand.Parameters.AddWithValue(
                "@CUSTOMER_ID", newCUSTOMER_ID);
            insertCommand.Parameters.AddWithValue(
                "@CUSTOMER_FIRST_NAME", customer.CUSTOMER_FIRST_NAME);
            insertCommand.Parameters.AddWithValue(
                "@CUSTOMER_LAST_NAME", customer.CUSTOMER_LAST_NAME);
            insertCommand.Parameters.AddWithValue(
                "@CUSTOMER_STREET_NAME", customer.CUSTOMER_STREET_NAME);
            insertCommand.Parameters.AddWithValue(
                "@CUSTOMER_STREET_NUMBER", customer.CUSTOMER_STREET_NUMBER);
            insertCommand.Parameters.AddWithValue(
                "@CUSTOMER_CITY", customer.CUSTOMER_CITY);
            insertCommand.Parameters.AddWithValue(
                "@CUSTOMER_STATE", customer.CUSTOMER_STATE);
            insertCommand.Parameters.AddWithValue(
                "@CUSTOMER_PHONE", customer.CUSTOMER_PHONE);
            insertCommand.Parameters.AddWithValue(
                "@CUSTOMER_REFERRED_BY", customer.CUSTOMER_REFERRED_BY);

            //This try statement selects our information and converts to Int32.
            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
                string strSelectStatement =
                    "SELECT MAX(CUSTOMER_ID) FROM customer";
                MySqlCommand selectCommand =
                    new MySqlCommand(strSelectStatement, connection);
                int CUSTOMER_ID = Convert.ToInt32(selectCommand.ExecuteScalar());
                return CUSTOMER_ID;
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
        /// <summary>
        /// This method gets the max customer ID in the table.
        /// </summary>
        /// <returns>Max Customer ID</returns>
        private static int GetMaxCustomer_ID()
        {
            //Identifies the max customer ID.
            MySqlConnection connection = Wildcat.GetConnection();
            string selectStatement = "Select Max(CUSTOMER_ID) From customer";

            try
            {
                connection.Open();
                MySqlCommand selectCommand = new MySqlCommand(selectStatement, connection);
                int CUSTOMER_ID = Convert.ToInt32(selectCommand.ExecuteScalar());
                return CUSTOMER_ID; 
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
        /// <summary>
        /// This method creates a customer ID for an added customer.
        /// </summary>
        /// <param></param>
        /// <returns>New Customer ID</returns>
        public static int createCustomerID(ref int CUSTOMER_ID)
        {
            //Increments the max customer ID by 1 and generate new ID for next customer.
            MySqlConnection connection = Wildcat.GetConnection();
            connection.Open();
            string selectStatement =
                "Select MAX(CUSTOMER_ID) FROM customer ";
            MySqlCommand selectCommand =
                new MySqlCommand(selectStatement, connection);
            CUSTOMER_ID = Convert.ToInt32(selectCommand.ExecuteScalar());
            CUSTOMER_ID++;
            connection.Close();
            return CUSTOMER_ID;
        }

        /// <summary>
        /// This boolean method updates our customer information. It includes code for both the new and old customer information.
        /// </summary>
        /// <param>@CUSTOMER_ID, @NewCUSTOMER_FIRST_NAME, @NewCUSTOMER_LAST_NAME, @NewCUSTOMER_STREET_NAME, @NewCUSTOMER_STREET_NUMBER, @NewCUSTOMER_CITY, @NewCUSTOMER_STATE, @NewCUSTOMER_PHONE, @NewCUSTOMER_REFERRED_BY</param>
        /// <param>@OldCUSTOMER_ID, @OldCUSTOMER_FIRST_NAME, @OldCUSTOMER_LAST_NAME, @OldCUSTOMER_STREET_NAME, @OldCUSTOMER_STREET_NUMBER, @OldCUSTOMER_CITY, @OldCUSTOMER_STATE, @OldCUSTOMER_PHONE, @OldCUSTOMER_REFERRED_BY</param>
        /// <returns></returns>
        public static bool blnUpdateCustomer(Customer oldCustomer, 
            Customer newCustomer)
        {
            MySqlConnection connection = Wildcat.GetConnection();
            //The following string is the updated customer information.
            string strUpdateStatement =
                "UPDATE customer SET " +
                "CUSTOMER_FIRST_NAME = @NewCUSTOMER_FIRST_NAME, " +
                "CUSTOMER_LAST_NAME = @NewCUSTOMER_LAST_NAME, " +
                "CUSTOMER_STREET_NAME = @NewCUSTOMER_STREET_NAME, " +
                "CUSTOMER_STREET_NUMBER = @NewCUSTOMER_STREET_NUMBER, " +
                "CUSTOMER_CITY = @NewCUSTOMER_CITY, " +
                "CUSTOMER_STATE = @NewCUSTOMER_STATE, " +
                "CUSTOMER_PHONE = @NewCUSTOMER_PHONE " +
                "WHERE CUSTOMER_ID = @OldCUSTOMER_ID" +
                " AND CUSTOMER_ID = @NewCUSTOMER_ID" +
                " AND CUSTOMER_FIRST_NAME = @OldCUSTOMER_FIRST_NAME " +
                " AND CUSTOMER_LAST_NAME = @OldCUSTOMER_LAST_NAME" +
                " AND CUSTOMER_STREET_NAME = @OldCUSTOMER_STREET_NAME " +
                " AND CUSTOMER_STREET_NUMBER = @OldCUSTOMER_STREET_NUMBER" +
                " AND CUSTOMER_CITY = @OldCUSTOMER_CITY " +
                " AND CUSTOMER_STATE = @OldCUSTOMER_STATE " +
                " AND CUSTOMER_PHONE = @OldCUSTOMER_PHONE";
            MySqlCommand updateCommand =
                new MySqlCommand(strUpdateStatement, connection);
            updateCommand.Parameters.AddWithValue(
                "@NewCUSTOMER_ID", newCustomer.CUSTOMER_ID);
            updateCommand.Parameters.AddWithValue(
                "@NewCUSTOMER_FIRST_NAME", newCustomer.CUSTOMER_FIRST_NAME);
            updateCommand.Parameters.AddWithValue(
                "@NewCUSTOMER_LAST_NAME", newCustomer.CUSTOMER_LAST_NAME);
            updateCommand.Parameters.AddWithValue(
                "@NewCUSTOMER_STREET_NAME", newCustomer.CUSTOMER_STREET_NAME);
            updateCommand.Parameters.AddWithValue(
                "@NewCUSTOMER_STREET_NUMBER", newCustomer.CUSTOMER_STREET_NUMBER);
            updateCommand.Parameters.AddWithValue(
                "@NewCUSTOMER_CITY", newCustomer.CUSTOMER_CITY);
            updateCommand.Parameters.AddWithValue(
                "@NewCUSTOMER_STATE", newCustomer.CUSTOMER_STATE);
            updateCommand.Parameters.AddWithValue(
                "@NewCUSTOMER_PHONE", newCustomer.CUSTOMER_PHONE);
            updateCommand.Parameters.AddWithValue(
                "@OldCUSTOMER_ID", oldCustomer.CUSTOMER_ID);
            updateCommand.Parameters.AddWithValue(
                "@OldCUSTOMER_FIRST_NAME", oldCustomer.CUSTOMER_FIRST_NAME);
            updateCommand.Parameters.AddWithValue(
                "@OldCUSTOMER_LAST_NAME", oldCustomer.CUSTOMER_LAST_NAME);
            updateCommand.Parameters.AddWithValue(
                "@OldCUSTOMER_STREET_NAME", oldCustomer.CUSTOMER_STREET_NAME);
            updateCommand.Parameters.AddWithValue(
                "@OldCUSTOMER_STREET_NUMBER", oldCustomer.CUSTOMER_STREET_NUMBER);
            updateCommand.Parameters.AddWithValue(
                "@OldCUSTOMER_CITY", oldCustomer.CUSTOMER_CITY);
            updateCommand.Parameters.AddWithValue(
                "@OldCUSTOMER_STATE", oldCustomer.CUSTOMER_STATE);
            updateCommand.Parameters.AddWithValue(
                "@OldCUSTOMER_PHONE", oldCustomer.CUSTOMER_PHONE);
            //The try catch is making sure the modify/update statement is working correctly.
            try
            {
                connection.Open();
                int intCount = updateCommand.ExecuteNonQuery();
                if (intCount > 0)
                    return true;
                else
                    return false;
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
     }
  }

