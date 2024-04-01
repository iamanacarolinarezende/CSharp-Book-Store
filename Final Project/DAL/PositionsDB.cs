using Final_Project.BLL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.DAL
{
    public class PositionsDB
    {
        public static List<Positions> GetAllPositions()
        {
            List<Positions> listP = new List<Positions>();
            SqlConnection conn = UtilityDB.GetDBConnection();
            SqlCommand cmdSelectAll = new SqlCommand("SELECT * FROM Positions", conn);

            SqlDataReader reader = cmdSelectAll.ExecuteReader();
            Positions pos;
            while (reader.Read())
            {
                pos = new Positions();
                pos.PositionID = Convert.ToInt32(reader["PositionID"]);
                pos.PositionName = reader["PositionName"].ToString();
                listP.Add(pos);
            }
            conn.Close();
            return listP;
        }
    }
}
