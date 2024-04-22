using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_Project.DAL;
using Final_Project.BLL;
using System.Security.Cryptography;

namespace Final_Project.DAL
{
    public class BookDB
    {
        public static void SaveBook(Book book, List<int> selectedAuthorIDs)
        {
            using (SqlConnection conn = UtilityDB.GetDBConnection())
            {

                // Insert Book at Books Table
                SqlCommand cmdInsertBook = new SqlCommand();
                cmdInsertBook.Connection = conn;
                cmdInsertBook.CommandText = "INSERT INTO Books (ISBN, Title, UnitPrice, YearPublished, QOH, Publisher) " +
                                            "VALUES (@ISBN, @Title, @UnitPrice, @YearPublished, @QOH, @Publisher);";
                cmdInsertBook.Parameters.AddWithValue("@ISBN", book.ISBN);
                cmdInsertBook.Parameters.AddWithValue("@Title", book.Title);
                cmdInsertBook.Parameters.AddWithValue("@UnitPrice", book.UnitPrice);
                cmdInsertBook.Parameters.AddWithValue("@YearPublished", book.YearPublished);
                cmdInsertBook.Parameters.AddWithValue("@QOH", book.QOH);
                cmdInsertBook.Parameters.AddWithValue("@Publisher", book.Publisher);
                cmdInsertBook.ExecuteNonQuery();

                // Populate BookAuthors
                foreach (int authorID in selectedAuthorIDs)
                {
                    SqlCommand cmdInsertBookAuthor = new SqlCommand();
                    cmdInsertBookAuthor.Connection = conn;
                    cmdInsertBookAuthor.CommandText = "INSERT INTO BookAuthors (ISBN, AuthorID) VALUES (@ISBN, @AuthorID);";
                    cmdInsertBookAuthor.Parameters.AddWithValue("@ISBN", book.ISBN);
                    cmdInsertBookAuthor.Parameters.AddWithValue("@AuthorID", authorID);
                    cmdInsertBookAuthor.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        public static void UpdateBook(Book book, List<int> selectedAuthorIDs)
        {
            using (SqlConnection conn = UtilityDB.GetDBConnection())
            {
                SqlCommand cmdUpdateBook = new SqlCommand();
                cmdUpdateBook.Connection = conn;
                cmdUpdateBook.CommandText = "UPDATE Books SET Title = @Title, UnitPrice = @UnitPrice, YearPublished = @YearPublished, QOH = @QOH, Publisher = @Publisher WHERE ISBN = @ISBN;";
                cmdUpdateBook.Parameters.AddWithValue("@Title", book.Title);
                cmdUpdateBook.Parameters.AddWithValue("@UnitPrice", book.UnitPrice);
                cmdUpdateBook.Parameters.AddWithValue("@YearPublished", book.YearPublished);
                cmdUpdateBook.Parameters.AddWithValue("@QOH", book.QOH);
                cmdUpdateBook.Parameters.AddWithValue("@Publisher", book.Publisher);
                cmdUpdateBook.Parameters.AddWithValue("@ISBN", book.ISBN);
                cmdUpdateBook.ExecuteNonQuery();

                // Clean the authors at BookAuthors Table
                SqlCommand cmdDeleteBookAuthors = new SqlCommand();
                cmdDeleteBookAuthors.Connection = conn;
                cmdDeleteBookAuthors.CommandText = "DELETE FROM BookAuthors WHERE ISBN = @ISBN;";
                cmdDeleteBookAuthors.Parameters.AddWithValue("@ISBN", book.ISBN);
                cmdDeleteBookAuthors.ExecuteNonQuery();

                // Isert the new authors at BookAuthors Table
                foreach (int authorID in selectedAuthorIDs)
                {
                    SqlCommand cmdInsertBookAuthor = new SqlCommand();
                    cmdInsertBookAuthor.Connection = conn;
                    cmdInsertBookAuthor.CommandText = "INSERT INTO BookAuthors (ISBN, AuthorID) VALUES (@ISBN, @AuthorID);";
                    cmdInsertBookAuthor.Parameters.AddWithValue("@ISBN", book.ISBN);
                    cmdInsertBookAuthor.Parameters.AddWithValue("@AuthorID", authorID);
                    cmdInsertBookAuthor.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        public static void DeleteBook(string ISBN)
        {
            using (SqlConnection conn = UtilityDB.GetDBConnection())
            {
                // Delete the associated authors from the BookAuthors table
                SqlCommand cmdDeleteBookAuthors = new SqlCommand();
                cmdDeleteBookAuthors.Connection = conn;
                cmdDeleteBookAuthors.CommandText = "DELETE FROM BookAuthors WHERE ISBN = @ISBN;";
                cmdDeleteBookAuthors.Parameters.AddWithValue("@ISBN", ISBN);
                cmdDeleteBookAuthors.ExecuteNonQuery();

                // Delete the book from the Books table
                SqlCommand cmdDeleteBook = new SqlCommand();
                cmdDeleteBook.Connection = conn;
                cmdDeleteBook.CommandText = "DELETE FROM Books WHERE ISBN = @ISBN;";
                cmdDeleteBook.Parameters.AddWithValue("@ISBN", ISBN);
                cmdDeleteBook.ExecuteNonQuery();


                conn.Close();
            }
        }



        //Search by ISBN, Book Title, Year
        public static List<Book> SearchRecord(string inputBook)
        {
            List<Book> listB = new List<Book>();
            using (SqlConnection conn = UtilityDB.GetDBConnection())
            {
                SqlCommand cmdSearchBy = new SqlCommand();
                cmdSearchBy.Connection = conn;
                cmdSearchBy.CommandText = "SELECT b.*, ba.AuthorID " +
                                          "FROM Books b " +
                                          "JOIN BookAuthors ba ON b.ISBN = ba.ISBN " +
                                          "WHERE b.ISBN LIKE '%' + @SearchText + '%' " +
                                          "OR b.Title LIKE '%' + @SearchText + '%' " +
                                          "OR b.YearPublished LIKE '%' + @SearchText + '%';";

                cmdSearchBy.Parameters.AddWithValue("@SearchText", inputBook);

                SqlDataReader reader = cmdSearchBy.ExecuteReader();

                while (reader.Read())
                {
                    Book book = new Book();
                    book.ISBN = reader["ISBN"].ToString();
                    book.Title = reader["Title"].ToString();
                    book.UnitPrice = Convert.ToDecimal(reader["UnitPrice"]); 
                    book.YearPublished = Convert.ToInt32(reader["YearPublished"]);
                    book.QOH = Convert.ToInt32(reader["QOH"]);
                    book.Publisher = Convert.ToInt32(reader["Publisher"].ToString());
                    book.AuthorID = Convert.ToInt32(reader["AuthorID"]); 
                    listB.Add(book);
                }

                conn.Close();
            }

            return listB;
        }

        public static bool IsUniqueISBN(string isbn)
        {
            bool isUnique = false;

            using (SqlConnection conn = UtilityDB.GetDBConnection())
            {
                SqlCommand cmdCheckISBN = new SqlCommand();
                cmdCheckISBN.Connection = conn;
                cmdCheckISBN.CommandText = "SELECT COUNT(*) FROM Books WHERE ISBN = @ISBN";
                cmdCheckISBN.Parameters.AddWithValue("@ISBN", isbn);

                int count = (int)cmdCheckISBN.ExecuteScalar();
                isUnique = count == 0;
            }

            return isUnique;
        }

        public static List<Book> GetAllRecords()
        {
            List<Book> listB = new List<Book>();
            using (SqlConnection conn = UtilityDB.GetDBConnection())
            {
                SqlCommand cmdSelectAll = new SqlCommand(
                    "SELECT b.ISBN, b.Title, b.UnitPrice, b.YearPublished, b.QOH, b.Publisher, ba.AuthorID " +
                    "FROM Books b " +
                    "JOIN BookAuthors ba ON b.ISBN = ba.ISBN", conn);

                SqlDataReader reader = cmdSelectAll.ExecuteReader();
                while (reader.Read())
                {
                    Book book = new Book();
                    book.ISBN = reader["ISBN"].ToString();
                    book.Title = reader["Title"].ToString();
                    book.UnitPrice = Convert.ToDecimal(reader["UnitPrice"]);
                    book.YearPublished = Convert.ToInt32(reader["YearPublished"]);
                    book.QOH = Convert.ToInt32(reader["QOH"]);
                    book.Publisher = Convert.ToInt32(reader["Publisher"]);
                    book.AuthorID = Convert.ToInt32(reader["AuthorID"]);
                    listB.Add(book);
                }
            }

            return listB;
        }


    }
}
