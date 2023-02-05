using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BiblioteksSida
{
    class BookSystem
    {
        string available_file = @"C:\Users\evdah\OneDrive\Skrivbord\BiblioteksSida\BiblioteksSida\BiblioteksSida\Files\AvailableBooks.txt";
        string borrowed_file = @"C:\Users\evdah\OneDrive\Skrivbord\BiblioteksSida\BiblioteksSida\BiblioteksSida\Files\Librarians.txt";
        string reserved_file = @"C:\Users\evdah\OneDrive\Skrivbord\BiblioteksSida\BiblioteksSida\BiblioteksSida\Files\Members.txt";

        private static BookSystem? instance = null;

        List<Book> available_books = new List<Book>();
        public List<Book> GetBooks() { return available_books; }

        private BookSystem()
        {
            LoadBooks();
        }

        public void AddBook(Book book)
        {
            available_books.Add(book);
            Save();
        }

        private void Save()
        {
            string[] aBooksStrArray = available_books.Select(book => $"{book.title}|{book.author}|{book.ISBN}").ToArray();

            File.WriteAllLines(available_file, aBooksStrArray);
        }
        private void LoadBooks()
        {
            LoadFile(available_file, available_books);
        }

        private void LoadFile(string file, List<Book> list)
        {
            string[] fileItems = System.IO.File.ReadAllLines(file);

            foreach (string item in fileItems)
            {
                string[] itemSplit = item.Split("|");
                string title = itemSplit[0];
                string author = itemSplit[1];
                string ISBN = itemSplit[2];

                Book book = new Book(title, author, ISBN);
                list.Add(book);
            }
        }

        public static BookSystem GetInstance()
        {
            if (instance == null)
            {
                instance = new BookSystem();
            }
            return instance;
        }
    }
}
