using Final_Project.BLL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Final_Project.DAL
{
    public class UserDB
    {
        //Save Users
        public static void SaveInfo(User user)
        {
            SqlConnection conn = UtilityDB.GetDBConnection();

            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.Connection = conn;
            cmdInsert.CommandText = "INSERT INTO Users (Username , Password, EmployeeID) " +
                                    "VALUES(@Username,@Password, @EmployeeID)";
            cmdInsert.Parameters.AddWithValue("@Username", user.UserName);
            cmdInsert.Parameters.AddWithValue("@Password", user.Password);
            cmdInsert.Parameters.AddWithValue("@EmployeeID", user.EmployeeID);
            cmdInsert.ExecuteNonQuery();
            
            conn.Close();
        }

        //List Users
        public static List<User> GetAllInfos()
        {
            List<User> listUser = new List<User>();
            SqlConnection conn = UtilityDB.GetDBConnection();
            SqlCommand cmdSelectAll = new SqlCommand("SELECT * FROM Users", conn);
            SqlDataReader reader = cmdSelectAll.ExecuteReader(); //APPLIES TO SELECT
            User user;
            while (reader.Read())
            {
                user = new User();
                user.UserName = reader["Username"].ToString();
                user.EmployeeID = reader["EmployeeID"].ToString();
                listUser.Add(user);
            }
            conn.Close();
            return listUser;
        }

        //Search By
        public static User SearchRecord(string UsName)
        {
            User user = new User();
            SqlConnection conn = UtilityDB.GetDBConnection();
            SqlCommand cmdSearchByID = new SqlCommand();
            cmdSearchByID.Connection = conn;
            cmdSearchByID.CommandText = "SELECT * FROM Users " +
                                        "WHERE Username=@Username";
            cmdSearchByID.Parameters.AddWithValue("@Username", UsName);
            SqlDataReader reader = cmdSearchByID.ExecuteReader();
            if (reader.Read())
            {
                user.UserName = reader["Username"].ToString().Trim();
                user.EmployeeID = reader["EmployeeID"].ToString();

            }
            else
            {
                user = null;
            }
            conn.Close();
            return user;
        }

        //Update User
        public static void UpdateRecord(User userUpdate)
        {
            // Step 1 : Open DB
            SqlConnection conn = UtilityDB.GetDBConnection();

            //Step 2: Perform INSERT operation
            // Create and customize an object of type SqlCommand
            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.Connection = conn;
            cmdInsert.CommandText = "UPDATE Users " + //@Password, @EmployeeID
                                    "    set Username=@Username," +
                                    "        Password=@Password," +
                                    "        EmployeeID=@EmployeeID " +
                                    " WHERE  Username=@Username";
            cmdInsert.Parameters.AddWithValue("@Username", userUpdate.UserName);
            cmdInsert.Parameters.AddWithValue("@Password", userUpdate.Password);
            cmdInsert.Parameters.AddWithValue("@EmployeeID", userUpdate.EmployeeID);
            cmdInsert.ExecuteNonQuery();
            //Step 3 : Close DB
            conn.Close();
        }

        //Delete User
        public static void DeleteRecord(string Username)
        {
            SqlConnection conn = UtilityDB.GetDBConnection();

            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.Connection = conn;
            cmdInsert.CommandText = "DELETE Users " +
                                     "WHERE Username=@Username";
            cmdInsert.Parameters.AddWithValue("@Username", Username);
            cmdInsert.ExecuteNonQuery();
           
            conn.Close();
        }
      

        //Is Unique
        public static bool IsUniqueUserName(string UsName)
        {
            User user = SearchRecord(UsName);
            if (user != null)
            {
                return false;
            }
            return true;
        }

        //Employee Exist
        public static bool ExistEmployeeID(string employeeID)
        {
            SqlConnection conn = UtilityDB.GetDBConnection();
            SqlCommand cmdCheckExistence = new SqlCommand();
            cmdCheckExistence.Connection = conn;
            cmdCheckExistence.CommandText = "SELECT COUNT(*) FROM Employees WHERE EmployeeID = @EmployeeID";
            cmdCheckExistence.Parameters.AddWithValue("@EmployeeID", employeeID);

            int count = (int)cmdCheckExistence.ExecuteScalar();

            conn.Close();

            // If count is greater than 0, it means that the Username exists in the Users table
            return count > 0;
        }

        //Update or Delete User by UserName
        public static bool ExistUsernameUser(string UsName)
        {
            User user = SearchRecord(UsName);
            if (user != null)
            {
                return true;
            }
            return false;
        }

    }
}
