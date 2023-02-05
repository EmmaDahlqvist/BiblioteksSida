using System;
using System.Collections.Generic;
using System.Threading;

namespace BiblioteksSida
{
    class Program
    {
        static UserSystem userSystem = UserSystem.GetInstance();
        static BookSystem bookSystem = BookSystem.GetInstance();
        //här sköts text och program steg
        static void Main(string[] args)
        {
            Console.WriteLine("1) Logga in\n2) Registrera dig");
            int choice = Options(2);
            switch (choice)
            {
                case 1:
                    LoginStage();
                    break;
                case 2:
                    RegistrationStage();
                    break;
            }
            
        }

        private static void MemberProfileStage(User user)
        {
            Console.WriteLine("memberprofile");
        }

        private static void LibrarianProfileStage(User user)
        {
            Console.Clear();
            Console.WriteLine("Din Sida - " + user.name);
            Console.WriteLine("1) Hantera böcker\n2) Hantera användare\n3) Hantera konto");
            int choice = Options(3);
            switch (choice)
            {
                case 1:
                    LibBookStage();
                    LibrarianProfileStage(user);
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
        }

        private static void LibBookStage()
        {
            Console.Clear();
            Console.WriteLine("Hantera böcker");
            Console.WriteLine("1) Lägg till bok\n2) Ta bort bok\n3) Se boklista");
            int choice = Options(3);
            switch (choice)
            {
                case 1:
                    AddBook();
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
        }

        private static void AddBook()
        {
            Book book;
            bool wrongISBN = false;
            string wrongISBNStr = "";

            Console.Clear();
            do
            {
                if (wrongISBN)
                {
                    Console.WriteLine($"ISBN för denna bok är {wrongISBNStr}, vänligen försök igen!\n");
                    wrongISBN = false;
                }
                Console.Write("Titel: ");
                string title = Console.ReadLine();
                Console.Write("Författare: ");
                string author = Console.ReadLine();
                Console.Write("ISBN: ");
                string ISBN = Console.ReadLine();

                book = new Book(title, author, ISBN);
                List<Book> a_books = bookSystem.GetBooks();

                foreach(Book a_book in a_books)
                {
                    if(a_book.title == title && a_book.author == author)
                    {
                        if(a_book.ISBN != ISBN)
                        {
                            wrongISBN = true;
                            wrongISBNStr = a_book.ISBN;
                        }
                    }
                }

            } while (wrongISBN);

            bookSystem.AddBook(book);
        }

        private static void LoginStage()
        {
            Console.Clear();
            do
            {
                Console.WriteLine("1) Medlem\n2) Bibliotikarie\n");
                int authority = Options(2);

                Console.Write("Lösenord: ");
                string password = Console.ReadLine();
                Console.Write("Personnummer: ");
                string personal_number = Console.ReadLine();

                //kolla om användaren finns
                User user = userSystem.CheckLogin(userSystem.GetMembers(), password, personal_number);

                if(user != null)
                {
                    if(authority == 1)
                    {
                        MemberProfileStage(user);
                        break;
                    } else if(authority == 2)
                    {
                        LibrarianProfileStage(user);
                        break;
                    }
                }

                Console.WriteLine("Uppgifterna stämmer ej, försök igen");
            } while (true);
        }

        public static void RegistrationStage()
        {
            Console.Clear();
            Console.WriteLine("Registrering");

            do
            {
                Console.Write("Namn: ");
                string name = Console.ReadLine();
                Console.Write("Lösenord: ");
                string password = Console.ReadLine();
                Console.Write("Personnummer: ");
                string personal_number = Console.ReadLine();

                User user = new User(name, password, personal_number);
                if (userSystem.PersonalNumberUnique(personal_number))
                {
                    userSystem.AddMember(user);
                    Console.WriteLine("Du är nu registrerad " + name);
                    Thread.Sleep(2000);
                    LoginStage();
                    break;
                }

                Console.WriteLine("Personnummret måste vara unikt! Försök igen");

            } while (true);
        }

        private static int Options(int amount)
        {
            int input = 0;
            bool correct = false;
            do
            {
                try
                {
                    input = int.Parse(Console.ReadLine());
                    for (int i = 1; i <= amount; i++)
                    {
                        if (input == i)
                        {
                            correct = true;
                        }
                    }
                    if (!correct)
                    {
                        Console.WriteLine("Välj ett efterfrågat alternativ!");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Vänligen välj en int!");
                }
            } while (!correct);

            return input;
        }
    }
}
