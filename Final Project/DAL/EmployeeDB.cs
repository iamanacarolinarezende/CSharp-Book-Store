using Final_Project.BLL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_Project.DAL;

namespace Final_Project.DAL
{
    public class EmployeeDB
    {
        public static void SaveEmployee(Employee emp)
        {
            SqlConnection conn = UtilityDB.GetDBConnection();

            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.Connection = conn;
            cmdInsert.CommandText = "INSERT INTO Employees (EmployeeID , FirstName, LastName, Phone, JobTitle) " +
                                    "VALUES(@EmployeeID , @FirstName, @LastName, @Phone, @JobTitle)";
            cmdInsert.Parameters.AddWithValue("@EmployeeID", emp.EmployeeID);
            cmdInsert.Parameters.AddWithValue("@FirstName", emp.FirstName);
            cmdInsert.Parameters.AddWithValue("@LastName", emp.LastName);
            cmdInsert.Parameters.AddWithValue("@Phone", emp.Phone);
            cmdInsert.Parameters.AddWithValue("@JobTitle", emp.JobTitle);
            cmdInsert.ExecuteNonQuery();

            conn.Close();
        }

        public static void UpdateRecord(Employee empUpdated)
        {
            SqlConnection conn = UtilityDB.GetDBConnection();

            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.Connection = conn;
            cmdInsert.CommandText = "UPDATE Employees " +
                                    "    set FirstName=@FirstName," +
                                    "        LastName=@LastName," +
                                    "        JobTitle=@JobTitle, " +
                                    "        Phone=@Phone " +
                                    " WHERE  EmployeeId=@EmployeeId";
            cmdInsert.Parameters.AddWithValue("@EmployeeId", empUpdated.EmployeeID);
            cmdInsert.Parameters.AddWithValue("@FirstName", empUpdated.FirstName);
            cmdInsert.Parameters.AddWithValue("@LastName", empUpdated.LastName);
            cmdInsert.Parameters.AddWithValue("@JobTitle", empUpdated.JobTitle);
            if (string.IsNullOrEmpty(empUpdated.Phone))
            {
                cmdInsert.Parameters.AddWithValue("@Phone", DBNull.Value); //Value for null or empty values
            }
            else
            {
                cmdInsert.Parameters.AddWithValue("@Phone", empUpdated.Phone);
            }
            cmdInsert.ExecuteNonQuery();
            
            conn.Close();
        }

        public static void DeleteRecord(int eId)
        {
            SqlConnection conn = UtilityDB.GetDBConnection();

            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.Connection = conn;
            cmdInsert.CommandText = "DELETE Employees " +
                                     "WHERE EmployeeId=@EmployeeId";
            cmdInsert.Parameters.AddWithValue("@EmployeeId", eId);
            cmdInsert.ExecuteNonQuery();
            
            conn.Close();
        }

        //Search by ID
        public static Employee SearchRecord(int empID)
        {
            Employee emp = new Employee();

            SqlConnection conn = UtilityDB.GetDBConnection();
            SqlCommand cmdSearchById = new SqlCommand();
            cmdSearchById.Connection = conn;
            cmdSearchById.CommandText = "SELECT * FROM Employees " + "WHERE EmployeeId=@EmployeeId";
            cmdSearchById.Parameters.AddWithValue("@EmployeeId", empID);

            SqlDataReader reader = cmdSearchById.ExecuteReader();
            if (reader.Read()) //if true
            {
                emp.EmployeeID = Convert.ToInt32(reader["EmployeeId"]);
                emp.FirstName = reader["FirstName"].ToString().Trim();
                emp.LastName = reader["LastName"].ToString();
                emp.JobTitle = Convert.ToInt32(reader["JobTitle"]);
                emp.Phone = reader["Phone"].ToString();
            }
            else //not found
            {
                emp = null;
            }

            conn.Close();
            return emp;
        }

        //Search by First or Last Name
        public static List<Employee> SearchRecord(string input)
        {
            List<Employee> listE = new List<Employee>();
            SqlConnection conn = UtilityDB.GetDBConnection();
            SqlCommand cmdSearchByName = new SqlCommand();
            cmdSearchByName.Connection = conn;
            cmdSearchByName.CommandText = "SELECT * FROM Employees " +
                                          "WHERE FirstName = @FirstName " +
                                          "or LastName = @LastName ";

            cmdSearchByName.Parameters.AddWithValue("@FirstName", input);
            cmdSearchByName.Parameters.AddWithValue("@LastName", input);
            SqlDataReader reader = cmdSearchByName.ExecuteReader(); // applied to SELECT 
            Employee emp;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    emp = new Employee();
                    emp.EmployeeID = Convert.ToInt32(reader["EmployeeId"]);
                    emp.FirstName = reader["FirstName"].ToString();
                    emp.LastName = reader["LastName"].ToString();
                    emp.JobTitle = Convert.ToInt32(reader["JobTitle"]);
                    emp.Phone = reader["Phone"].ToString();
                    listE.Add(emp);
                }
            }
            conn.Close();
            return listE;
        }

        //First and Last Name
        public static List<Employee> SearchRecord(string input1, string input2) 
        {
            List<Employee> listE = new List<Employee>();
            SqlConnection conn = UtilityDB.GetDBConnection();
            SqlCommand cmdSearchByName = new SqlCommand();
            cmdSearchByName.Connection = conn;
            cmdSearchByName.CommandText = "SELECT * FROM Employees " +
                                          "WHERE FirstName = @FirstName " +
                                          "and LastName = @LastName ";

            cmdSearchByName.Parameters.AddWithValue("@FirstName", input1);
            cmdSearchByName.Parameters.AddWithValue("@LastName", input2);
            SqlDataReader reader = cmdSearchByName.ExecuteReader(); //Applied to SELECT
            Employee emp;
            while (reader.Read())
            {
                emp = new Employee();
                emp.EmployeeID = Convert.ToInt32(reader["EmployeeID"]);
                emp.FirstName = reader["FirstName"].ToString();
                emp.LastName = reader["LastName"].ToString();
                emp.JobTitle = Convert.ToInt32(reader["JobTitle"]);
                emp.Phone = reader["Phone"].ToString();
                listE.Add(emp);
            }
            conn.Close();
            return listE;
        }

        public static bool IsUniqueId(int eId)
        {
            Employee emp = SearchRecord(eId);
            if (emp != null)
            {
                return false;
            }
            return true;
        }

        public static List<Employee> GetAllRecords()
        {
            List<Employee> listE = new List<Employee>();
            SqlConnection conn = UtilityDB.GetDBConnection();
            SqlCommand cmdSelectAll = new SqlCommand("SELECT * FROM Employees", conn);

            SqlDataReader reader = cmdSelectAll.ExecuteReader(); 
            Employee emp;
            while (reader.Read())
            {
                emp = new Employee();
                emp.EmployeeID = Convert.ToInt32(reader["EmployeeID"]);
                emp.FirstName = reader["FirstName"].ToString();
                emp.LastName = reader["LastName"].ToString();
                emp.JobTitle = Convert.ToInt32(reader["JobTitle"]);
                emp.Phone = reader["Phone"].ToString();
                listE.Add(emp); 
            }
            conn.Close();
            return listE;
        }
    }
}
