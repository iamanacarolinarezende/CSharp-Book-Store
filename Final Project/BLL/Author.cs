using Final_Project.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.BLL
{
    public class Author
    {
        public int AuthorID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string LastNameFirstName => LastName + ", " + FirstName;

        //List all Authos
        public static List<Author> GetAuthorList() => AuthorDB.GetAllAuthors();

        //Add Author
        public void AddAuthor(Author author) => AuthorDB.SaveNewAuthor(author);
    }
}
