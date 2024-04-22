using Final_Project.DAL;
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
        public int Publisher { get; set; }
        public int AuthorID { get; set; }

        //Save Book
        public void SaveBooks(Book bk, List<int> authors) => BookDB.SaveBook(bk, authors);

        //Update Book
        public void UpdateBooks(Book bk, List<int> selectedAuthorIDs) => BookDB.UpdateBook(bk, selectedAuthorIDs);

        //Delete Book
        public void DeleteBooks(String bkId) => BookDB.DeleteBook(bkId);

        //Unique ISBN
        public bool UniqueISBN(string ISBN) => BookDB.IsUniqueISBN(ISBN);

        //List all Books
        public List<Book> GetBookList() => BookDB.GetAllRecords();

        //Search books
        public List<Book> SearchBooks(string inputBook) => BookDB.SearchRecord(inputBook);

    }
}
