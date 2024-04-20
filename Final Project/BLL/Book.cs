using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.BLL
{
    public class Book
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public decimal UnitPrice { get; set; }
        public int YearPublished { get; set; }
        public int QOH { get; set; }
        public string Category { get; set; }
        public string Publisher { get; set; }


    }
}
