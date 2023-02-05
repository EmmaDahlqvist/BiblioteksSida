using System;
using System.Collections.Generic;
using System.Text;

namespace BiblioteksSida
{
    class Book
    {
        public string title;
        public string author;
        public string ISBN;

        public Book(string title, string author, string ISBN)
        {
            this.title = title;
            this.author = author;
            this.ISBN = ISBN;
        }
    }
}
