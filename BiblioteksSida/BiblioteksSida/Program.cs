using System;

namespace BiblioteksSida
{
    class Program
    {
        static UserSystem userSystem = UserSystem.GetInstance();
        static void Main(string[] args)
        {
            //här sköts text
            LoginStage();
            
        }

        private static void LoginStage()
        {
            foreach (User users in userSystem.GetMembers())
            {
                Console.WriteLine(users.name + users.password + users.personal_number);
            }

            Console.WriteLine("INLOGGNING");
            Console.Write("Lösenord: ");
            string password = Console.ReadLine();
            Console.Write("Personnummer: ");
            string personal_number = Console.ReadLine();

            foreach(User user in userSystem.GetMembers())
            {
                if(user.password == password && user.personal_number == personal_number)
                {
                    Console.WriteLine("Correct");
                    MemberProfileStage();
                    
                }
            }
            Console.WriteLine("Incorrect");
        }

        private static void MemberProfileStage()
        {

        }

        public void RegistrationStage()
        {

        }
    }
}
