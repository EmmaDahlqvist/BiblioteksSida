using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BiblioteksSida
{
    class UserSystem
    {
        //här hanteras alla användar uppgifter/filer

        private static UserSystem? instance = null;

        string members_file = @"C:\Users\evdah\OneDrive\Skrivbord\BiblioteksSida\BiblioteksSida\BiblioteksSida\Files\Members.txt";
        string librarians_file = @"C:\Users\evdah\OneDrive\Skrivbord\BiblioteksSida\BiblioteksSida\BiblioteksSida\Files\Librarians.txt";

        List<User> members = new List<User>();
        public List<User> GetMembers() { return members; }

        List<User> librarians = new List<User>();
        public List<User> GetLibrarians() { return librarians; }

        private UserSystem()
        {
            LoadUsers();
        }

        public void AddMember(User member)
        {
            members.Add(member);
            Save();
        }
        public void AddLibrarian(User member)
        {
            members.Add(member);
            Save();
        }

        private void Save()
        {
            string[] membersStringArray = members.Select(member => $"{member.name}|{member.password}|{member.personal_number}").ToArray();
            string[] librariansStringArray = librarians.Select(librarian => $"{librarian.name}|{librarian.password}|{librarian.personal_number}").ToArray();

            File.WriteAllLines(members_file, membersStringArray);
            File.WriteAllLines(librarians_file, librariansStringArray);
        }

        public void LoadUsers()
        {
            LoadFile(members_file, members);
            LoadFile(librarians_file, librarians);
        }

        private void LoadFile(string file, List<User> list)
        {
            string[] fileItems = System.IO.File.ReadAllLines(file);
            
            foreach(string item in fileItems)
            {
                string[] itemSplit = item.Split("|");
                string name = itemSplit[0];
                string password = itemSplit[1];
                string personal_number = itemSplit[2];
                
                User user = new User(name, password, personal_number);
                list.Add(user);
            }
        }

        public static UserSystem GetInstance()
        {
            if(instance == null)
            {
                instance = new UserSystem();
            }
            return instance;
        }

        public User CheckLogin(List<User> users, string password, string personal_number)
        {
            foreach (User user in users)
            {
                if (user.password == password && user.personal_number == personal_number)
                {
                    return user;

                }
            }
            return null;
        }

        public bool PersonalNumberUnique(string personal_number)
        {
            foreach(User user in members)
            {
                if(user.personal_number == personal_number)
                {
                    return false; //registrering går ej igenom
                }
            }

            return true;
        }
    }
}
