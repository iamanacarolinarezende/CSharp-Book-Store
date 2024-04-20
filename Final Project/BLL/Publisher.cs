using Final_Project.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.BLL
{
    public class Publisher
    {
        public int PublisherID { get; set; }
        public string PublisherName { get; set; }

        //List all Positions
        public static List<Publisher> GetPublisherList() => PublisherDB.GetAllPublishers();

        //Add Publisher
        public void AddPublisher(Publisher publisher) => PublisherDB.SaveNewPublisher(publisher);
    }
}
