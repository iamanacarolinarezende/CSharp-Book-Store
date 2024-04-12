using Final_Project.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Final_Project.DAL
{
    public class UserDB
    {
        //Save Users
        public static void SaveInfo(User user)
        {
            SqlConnection conn = UtilityDB.GetDBConnection();

            //Save the User info
            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.Connection = conn;
            cmdInsert.CommandText = "INSERT INTO Users (Username , Password, EmployeeIDUser) " +
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
                user.EmployeeID = reader["EmployeeIDUser"].ToString();
                listUser.Add(user);
            }
            conn.Close();
            return listUser;
        }

        //Search By EmployeeID
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
                user.Password = string.Empty;
                user.EmployeeID = reader["EmployeeIDUser"].ToString();
            }
            else
            {
                user = null;
            }
            conn.Close();
            return user;
        }

        //Search by Employee UserName
        public static User SearchRecordEID(string UsName)
        {
            User user = new User();
            SqlConnection conn = UtilityDB.GetDBConnection();
            SqlCommand cmdSearchByID = new SqlCommand();
            cmdSearchByID.Connection = conn;
            cmdSearchByID.CommandText = "SELECT * FROM Users " +
                                        "WHERE EmployeeIDUser=@EmployeeID";
            cmdSearchByID.Parameters.AddWithValue("@EmployeeID", UsName);
            SqlDataReader reader = cmdSearchByID.ExecuteReader();
            if (reader.Read())
            {
                user.UserName = reader["Username"].ToString().Trim();
                user.Password = string.Empty;
                user.EmployeeID = reader["EmployeeIDUser"].ToString();
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
            SqlConnection conn = UtilityDB.GetDBConnection();
            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.Connection = conn;
            cmdInsert.CommandText = "UPDATE Users " + //@Password, @UserName
                                    "    set Username=@Username," +
                                    "        Password=@Password," +
                                    "        EmployeeIDUser=@EmployeeID " +
                                    " WHERE  EmployeeIDUser=@EmployeeID";
            cmdInsert.Parameters.AddWithValue("@Username", userUpdate.UserName);
            cmdInsert.Parameters.AddWithValue("@Password", userUpdate.Password);
            cmdInsert.Parameters.AddWithValue("@EmployeeID", userUpdate.EmployeeID);
            cmdInsert.ExecuteNonQuery();

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

        //Employee Exist at table of employee
        public static bool ExistEmployeeID_empTable(string employeeID)
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

        //Employee exist at table of users
        public static bool ExistEmpIdUser(string UsIDUser)
        {
            SqlConnection conn = UtilityDB.GetDBConnection();
            SqlCommand cmdCheckExistence = new SqlCommand();
            cmdCheckExistence.Connection = conn;
            cmdCheckExistence.CommandText = "SELECT COUNT(*) FROM Users WHERE EmployeeIDUser = @EmployeeID";
            cmdCheckExistence.Parameters.AddWithValue("@EmployeeID", UsIDUser);

            int count = (int)cmdCheckExistence.ExecuteScalar();

            conn.Close();

            // If count is greater than 0, it means that the EmployeeID already has a user associated with it
            return count > 0;
        }

        //Check if Username and Password is correct
        public static bool UserExistsLogin(string username, string password)
        {
            SqlConnection conn = UtilityDB.GetDBConnection();
            SqlCommand cmdCheckExistence = new SqlCommand();
            cmdCheckExistence.Connection = conn;
            cmdCheckExistence.CommandText = "SELECT COUNT(*) FROM Users WHERE Username COLLATE Latin1_General_CS_AS = @Username AND Password COLLATE Latin1_General_CS_AS = @Password";
            cmdCheckExistence.Parameters.AddWithValue("@Username", username);
            cmdCheckExistence.Parameters.AddWithValue("@Password", password);

            int count = (int)cmdCheckExistence.ExecuteScalar();

            conn.Close();

            // If count is greater than 0, it means that the EmployeeID already has a user associated with it
            return count > 0;
        }

        public static string GetUserJobTitle(string username, string password)
        {
            string jobTitle = null;
            SqlConnection conn = UtilityDB.GetDBConnection();
            SqlCommand cmdGetJobTitle = new SqlCommand();
            cmdGetJobTitle.Connection = conn;
            cmdGetJobTitle.CommandText = "SELECT JobTitle FROM Employees INNER JOIN Users ON Employees.EmployeeID = Users.EmployeeIDUser WHERE Users.Username = @Username AND Users.Password = @Password";
            cmdGetJobTitle.Parameters.AddWithValue("@Username", username);
            cmdGetJobTitle.Parameters.AddWithValue("@Password", password);

            SqlDataReader reader = cmdGetJobTitle.ExecuteReader();
            if (reader.Read())
            {
                jobTitle = reader.GetInt32(0).ToString();
            }
            reader.Close();
            conn.Close();

            return jobTitle;
        }
    }
}
