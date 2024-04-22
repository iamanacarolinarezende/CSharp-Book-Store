using Final_Project.BLL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.DAL
{
    public class AuthorDB
    {
        //Add a new author
        public static void SaveNewAuthor(Author author)
        {
            SqlConnection conn = UtilityDB.GetDBConnection();

            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.Connection = conn;
            cmdInsert.CommandText = "INSERT INTO Authors (FirstName, LastName, Email) " +
                                    "VALUES (@FirstName, @LastName, @Email)";

            cmdInsert.Parameters.AddWithValue("@FirstName", author.FirstName);
            cmdInsert.Parameters.AddWithValue("@LastName", author.LastName);
            cmdInsert.Parameters.AddWithValue("@Email", author.Email);
            cmdInsert.ExecuteNonQuery();

            conn.Close();
        }

        //Get all Authors List
        public static List<Author> GetAllAuthors()
        {
            List<Author> listA = new List<Author>();
            SqlConnection conn = UtilityDB.GetDBConnection();
            SqlCommand cmdSelectAll = new SqlCommand("SELECT * FROM Authors", conn);

            SqlDataReader reader = cmdSelectAll.ExecuteReader();
            Author aut;
            while (reader.Read())
            {
                aut = new Author();
                aut.AuthorID = Convert.ToInt32(reader["AuthorID"]);
                aut.FirstName = reader["FirstName"].ToString();
                aut.LastName = reader["LastName"].ToString();
                aut.Email = reader["email"].ToString();
                listA.Add(aut);
            }
            conn.Close();
            return listA;
        }
    }
}
