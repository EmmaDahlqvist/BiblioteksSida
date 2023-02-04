using System;
using System.Collections.Generic;
using System.Text;

namespace BiblioteksSida
{
    class User
    {
        public string name;
        public string password;
        public string personal_number;

        public User(string name, string password, string personal_number)
        {
            this.name = name;
            this.password = password;
            this.personal_number = personal_number;
        }
    }
}
