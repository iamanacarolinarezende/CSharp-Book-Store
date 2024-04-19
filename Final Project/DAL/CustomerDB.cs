using Final_Project.BLL;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Final_Project.DAL
{
    public class CustomerDB
    {
        public static void SaveCustomerInfo(Customer customer)
        {
            SqlConnection conn = UtilityDB.GetDBConnection();

            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.Connection = conn;
            cmdInsert.CommandText = "INSERT INTO Customers (firstname, lastname, street, city, postal_code, phone_number, fax_number, credit_limit, status, email) " +
                           "VALUES (@Name, @Last, @Street, @City, @PostalCode, @PhoneNumber, @FaxNumber, @CreditLimit, @Status, @Email)";

            cmdInsert.Parameters.AddWithValue("@Name", customer.FirstName);
            cmdInsert.Parameters.AddWithValue("@Last", customer.LastName);
            cmdInsert.Parameters.AddWithValue("@Street", customer.Street);
            cmdInsert.Parameters.AddWithValue("@City", customer.City);
            cmdInsert.Parameters.AddWithValue("@PostalCode", customer.PostalCode);
            cmdInsert.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
            cmdInsert.Parameters.AddWithValue("@FaxNumber", customer.FaxNumber);
            cmdInsert.Parameters.AddWithValue("@CreditLimit", customer.CreditLimit);
            cmdInsert.Parameters.AddWithValue("@Email", customer.Email);
            cmdInsert.Parameters.AddWithValue("@Status", 1);
            cmdInsert.ExecuteNonQuery();

            conn.Close();
        }

        //List All Customer
        public static List<Customer> GetAllCustomers()
        {
            List<Customer> listCust = new List<Customer>();
            SqlConnection conn = UtilityDB.GetDBConnection();
            SqlCommand cmdSelectAll = new SqlCommand("SELECT * FROM Customers WHERE status = 1", conn);
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
                cust.DateTimeSince = Convert.ToDateTime(reader["clientSince"]);
                cust.Email = reader["email"].ToString();
                listCust.Add(cust);
            }
            conn.Close();
            return listCust;
        }

        //Update Customer
        public static void UpdateCustomerInfo(Customer customer)
        {
            SqlConnection conn = UtilityDB.GetDBConnection();
            SqlCommand cmdUpdate = new SqlCommand();
            cmdUpdate.Connection = conn;
            cmdUpdate.CommandText = "UPDATE Customers " +
                                    "SET firstname = @Name, " +
                                    "    lastname = @Last, " +
                                    "    street = @Street, " +
                                    "    city = @City, " +
                                    "    postal_code = @PostalCode, " +
                                    "    phone_number = @PhoneNumber, " +
                                    "    fax_number = @FaxNumber, " +
                                    "    credit_limit = @CreditLimit, " +
                                    "    email = @Email," +
                                    "    status = 1 " +
                                    "WHERE phone_number = @PhoneNumber";

            cmdUpdate.Parameters.AddWithValue("@Name", customer.FirstName);
            cmdUpdate.Parameters.AddWithValue("@Last", customer.LastName);
            cmdUpdate.Parameters.AddWithValue("@Street", customer.Street);
            cmdUpdate.Parameters.AddWithValue("@City", customer.City);
            cmdUpdate.Parameters.AddWithValue("@PostalCode", customer.PostalCode);
            cmdUpdate.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
            cmdUpdate.Parameters.AddWithValue("@FaxNumber", customer.FaxNumber);
            cmdUpdate.Parameters.AddWithValue("@CreditLimit", customer.CreditLimit);
            cmdUpdate.Parameters.AddWithValue("@Email", customer.Email);

            cmdUpdate.ExecuteNonQuery();
            conn.Close();
        }

        //Delete Customer
        public static void DeleteRecord(string Phone)
        {
            SqlConnection conn = UtilityDB.GetDBConnection();

            SqlCommand cmdDelete = new SqlCommand();
            cmdDelete.Connection = conn;
            cmdDelete.CommandText = "UPDATE Customers " +
                                    "SET status = 2" + //Change status to not delete the information
                                     "WHERE phone_number=@PhoneNumber";
            cmdDelete.Parameters.AddWithValue("@PhoneNumber", Phone);
            cmdDelete.ExecuteNonQuery();

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
                cust.Email = reader["email"].ToString();
            }
            else
            {
                cust = null;
            }
            conn.Close();
            return cust;
        }

        //Search by First or Last name
        public static List<Customer> SearchRecordName(string input)
        {
            List<Customer> listC = new List<Customer>();
            SqlConnection conn = UtilityDB.GetDBConnection();
            SqlCommand cmdSearchByName = new SqlCommand();
            cmdSearchByName.Connection = conn;
            cmdSearchByName.CommandText = "SELECT * FROM Customers " +
                                          "WHERE firstname LIKE '%' + @FirstName + '%' " +
                                          "OR lastname LIKE '%' + @LastName + '%'" +
                                          "OR city LIKE '%' + @City + '%' " +
                                          "OR postal_code = @Zip"; ;

            cmdSearchByName.Parameters.AddWithValue("@FirstName", input);
            cmdSearchByName.Parameters.AddWithValue("@LastName", input);
            cmdSearchByName.Parameters.AddWithValue("@City", input);
            cmdSearchByName.Parameters.AddWithValue("@Zip", input);
            SqlDataReader reader = cmdSearchByName.ExecuteReader();
            Customer cust;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cust = new Customer();
                    cust.FirstName = reader["firstname"].ToString();
                    cust.LastName = reader["lastname"].ToString();
                    cust.PhoneNumber = reader["phone_number"].ToString();
                    cust.FaxNumber = reader["fax_number"].ToString();
                    cust.Street = reader["street"].ToString();
                    cust.City = reader["city"].ToString();
                    cust.PostalCode = reader["postal_code"].ToString();
                    cust.CreditLimit = Convert.ToDecimal(reader["credit_limit"]);
                    cust.Email = reader["email"].ToString();
                    listC.Add(cust);
                }
            }
            conn.Close();
            return listC;
        }

        //Search by First AND Last Name
        public static List<Customer> SearchRecordFLName(string FName, string LName)
        {
            List<Customer> listC = new List<Customer>();
            SqlConnection conn = UtilityDB.GetDBConnection();
            SqlCommand cmdSearchByName = new SqlCommand();
            cmdSearchByName.Connection = conn;
            cmdSearchByName.CommandText = "SELECT * FROM Customers " +
                                          "WHERE firstname LIKE '%' + @FirstName + '%' " +
                                          "AND lastname LIKE '%' + @LastName + '%'";

            cmdSearchByName.Parameters.AddWithValue("@FirstName", FName);
            cmdSearchByName.Parameters.AddWithValue("@LastName", LName);
            SqlDataReader reader = cmdSearchByName.ExecuteReader();
            Customer cust;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cust = new Customer();
                    cust.FirstName = reader["firstname"].ToString();
                    cust.LastName = reader["lastname"].ToString();
                    cust.PhoneNumber = reader["phone_number"].ToString();
                    cust.FaxNumber = reader["fax_number"].ToString();
                    cust.Street = reader["street"].ToString();
                    cust.City = reader["city"].ToString();
                    cust.PostalCode = reader["postal_code"].ToString();
                    cust.CreditLimit = Convert.ToDecimal(reader["credit_limit"]);
                    cust.Email = reader["email"].ToString();
                    listC.Add(cust);
                }
            }
            conn.Close();
            return listC;
        }

        //Is Unique by Phone
        public static bool IsUniquePhoneNumber(string Phone)
        {
            Customer cust = SearchRecord(Phone);
            if (cust != null)
            {
                return false;
            }
            return true;
        }
        
        //Customer status 1 = Active | 2 = Inactive/Deleted
        public static bool IsCustomerActive (string Phone)
        {
            bool isValidCustomer = true; // Assume the customer is valid by default
            SqlConnection conn = UtilityDB.GetDBConnection();
            SqlCommand cmdSearchByPhone = new SqlCommand();
            cmdSearchByPhone.Connection = conn;
            cmdSearchByPhone.CommandText = "SELECT status FROM Customers " +
                                           "WHERE phone_number=@PhoneNumber";
            cmdSearchByPhone.Parameters.AddWithValue("@PhoneNumber", Phone);

            SqlDataReader reader = cmdSearchByPhone.ExecuteReader();
            if (reader.Read())
            {
                int status = Convert.ToInt32(reader["status"]);
                if (status == 2)
                {
                    isValidCustomer = false; // Set to false if status is 2
                }
            }
            conn.Close();
            return isValidCustomer;
        }

    }
}

