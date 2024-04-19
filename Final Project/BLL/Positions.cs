using Final_Project.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.BLL
{
    public class Positions
    {
        public int PositionID { get; set; }
        public string PositionName { get; set; }

        //List all Positions
        public List<Positions> GetPositionList() => PositionsDB.GetAllPositions();

        //Add Position
        public void AddPosition(Positions position) => PositionsDB.SaveNewPosition(position);
    }
}
