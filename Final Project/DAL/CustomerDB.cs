using Final_Project.BLL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.DAL
{
    public class CustomerDB
    {
        public static void SaveCustomerInfo(Customer customer)
        {
            SqlConnection conn = UtilityDB.GetDBConnection();

            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.Connection = conn;
            cmdInsert.CommandText = "INSERT INTO Customers (firstname, lastname, street, city, postal_code, phone_number, fax_number, credit_limit) " +
                           "VALUES (@Name, @Last, @Street, @City, @PostalCode, @PhoneNumber, @FaxNumber, @CreditLimit)";

            cmdInsert.Parameters.AddWithValue("@Name", customer.FirstName);
            cmdInsert.Parameters.AddWithValue("@Last", customer.LastName);
            cmdInsert.Parameters.AddWithValue("@Street", customer.Street);
            cmdInsert.Parameters.AddWithValue("@City", customer.City);
            cmdInsert.Parameters.AddWithValue("@PostalCode", customer.PostalCode);
            cmdInsert.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
            cmdInsert.Parameters.AddWithValue("@FaxNumber", customer.FaxNumber);
            cmdInsert.Parameters.AddWithValue("@CreditLimit", customer.CreditLimit);
            cmdInsert.ExecuteNonQuery();

            conn.Close();
        }

        //List All Customer
        public static List<Customer> GetAllCustomers()
        {
            List<Customer> listCust = new List<Customer>();
            SqlConnection conn = UtilityDB.GetDBConnection();
            SqlCommand cmdSelectAll = new SqlCommand("SELECT * FROM Customers", conn);
            SqlDataReader reader = cmdSelectAll.ExecuteReader(); 
            Customer cust;
            while (reader.Read())
            {
                cust = new Customer();
                cust.FirstName = reader["firstname"].ToString();
                cust.LastName = reader["lastname"].ToString();
                cust.PhoneNumber = reader["phone_number"].ToString();
                cust.FaxNumber = reader["fax_number"].ToString();
                cust.Street = reader["street"].ToString();
                cust.City = reader["city"].ToString() ;
                cust.PostalCode = reader["postal_code"].ToString();
                cust.CreditLimit = Convert.ToDecimal(reader["credit_limit"]);
                listCust.Add(cust);
            }
            conn.Close();
            return listCust;
        }

        //Update Customer
        public static void UpdateCustomer(Customer customer)
        {
            SqlConnection conn = UtilityDB.GetDBConnection();
            SqlCommand cmdUpdate = new SqlCommand();
            cmdUpdate.Connection = conn;
            cmdUpdate.CommandText = "UPDATE Customers " +
                                    "SET fisrtname = @Name, " +
                                    "    lastname = @Last, " +
                                    "    Street = @Street, " +
                                    "    City = @City, " +
                                    "    PostalCode = @PostalCode, " +
                                    "    PhoneNumber = @PhoneNumber, " +
                                    "    FaxNumber = @FaxNumber, " +
                                    "    CreditLimit = @CreditLimit " +
                                    "WHERE PhoneNumber = @PhoneNumber";

            cmdUpdate.Parameters.AddWithValue("@Name", customer.FirstName);
            cmdUpdate.Parameters.AddWithValue("@Last", customer.LastName);
            cmdUpdate.Parameters.AddWithValue("@Street", customer.Street);
            cmdUpdate.Parameters.AddWithValue("@City", customer.City);
            cmdUpdate.Parameters.AddWithValue("@PostalCode", customer.PostalCode);
            cmdUpdate.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
            cmdUpdate.Parameters.AddWithValue("@FaxNumber", customer.FaxNumber);
            cmdUpdate.Parameters.AddWithValue("@CreditLimit", customer.CreditLimit);

            cmdUpdate.ExecuteNonQuery();
            conn.Close();
        }

        //Delete Customer
        public static void DeleteRecord(string PhoneNumber)
        {
            SqlConnection conn = UtilityDB.GetDBConnection();

            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.Connection = conn;
            cmdInsert.CommandText = "DELETE Customers " +
                                     "WHERE phone_number=@PhoneNumber";
            cmdInsert.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
            cmdInsert.ExecuteNonQuery();

            conn.Close();
        }

        //Search By Phone Number
        public static Customer SearchRecord(string Phone)
        {
            Customer cust = new Customer();
            SqlConnection conn = UtilityDB.GetDBConnection();
            SqlCommand cmdSearchByPhone = new SqlCommand();
            cmdSearchByPhone.Connection = conn;
            cmdSearchByPhone.CommandText = "SELECT * FROM Customers " +
                                        "WHERE phone_number=@PhoneNumber";
            cmdSearchByPhone.Parameters.AddWithValue("@PhoneNumber", Phone);
            SqlDataReader reader = cmdSearchByPhone.ExecuteReader();
            if (reader.Read())
            {
                cust.FirstName = reader["firstname"].ToString();
                cust.LastName = reader["lastname"].ToString();
                cust.PhoneNumber = reader["phone_number"].ToString();
                cust.FaxNumber = reader["fax_number"].ToString();
                cust.Street = reader["street"].ToString();
                cust.City = reader["city"].ToString();
                cust.PostalCode = reader["postal_code"].ToString();
                cust.CreditLimit = Convert.ToDecimal(reader["credit_limit"]);
            }
            else
            {
                cust = null;
            }
            conn.Close();
            return cust;
        }

        //Is Unique
        public static bool IsUniquePhoneNumber(string Phone)
        {
            Customer cust = SearchRecord(Phone);
            if (cust != null)
            {
                return false;
            }
            return true;
        }
        

    }
}

