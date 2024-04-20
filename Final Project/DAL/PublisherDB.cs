using Final_Project.BLL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.DAL
{
    public class PublisherDB
    {
        public static List<Publisher> GetAllPublishers()
        {
            List<Publisher> listPub = new List<Publisher>();
            SqlConnection conn = UtilityDB.GetDBConnection();
            SqlCommand cmdSelectAll = new SqlCommand("SELECT * FROM Publishers", conn);

            SqlDataReader reader = cmdSelectAll.ExecuteReader();
            Publisher pub;
            while (reader.Read())
            {
                pub = new Publisher();
                pub.PublisherID = Convert.ToInt32(reader["PublisherID"]);
                pub.PublisherName = reader["CompanyName"].ToString();
                listPub.Add(pub);
            }
            conn.Close();
            return listPub;
        }

        //Add a new publisher
        public static void SaveNewPublisher(Publisher pub)
        {
            SqlConnection conn = UtilityDB.GetDBConnection();

            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.Connection = conn;
            cmdInsert.CommandText = "INSERT INTO Publishers (CompanyName) " + "VALUES (@CompanyName)";

            cmdInsert.Parameters.AddWithValue("@CompanyName", pub.PublisherName);
            cmdInsert.ExecuteNonQuery();

            conn.Close();
        }
    }
}
