using System.Runtime.CompilerServices;

namespace AuthenticationApp
{
    class Program
    {
        private static List<User> users = new();

        static void Main(string[] args)
        {
            ShowMainMenu();
        }

        private static void ShowMainMenu()
        {
            int input;
            do
            {
                Console.Clear();
                Console.WriteLine(DateTime.Now);
                Console.WriteLine("============= Basic Authentication ==================");
                Console.WriteLine("1. Create User");
                Console.WriteLine("2. Show User");
                Console.WriteLine("3. Search User");
                Console.WriteLine("4. Login User");
                Console.WriteLine("5. Exit");
                Console.Write("Input: ");

                input = Convert.ToInt32(Console.ReadLine());

                Console.Clear();

                switch (input)
                {
                    case 1:
                        CreateUser();
                        break;
                    case 2:
                        ShowUser();
                        break;
                    case 3:
                        SearchUser();
                        break;
                    case 4:
                        Login();
                        break;
                    case 5:
                        Console.WriteLine("Program clossed ...");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Command Not Found!");
                        break;
                }

            } while (input >= 5);

        }

        private static void CreateUser()
        {
            Console.Clear();
            Console.Write("First Name => ");
            string firstName = FirstNameValidation(Console.ReadLine());

            Console.Write("Last Name => ");
            string lastName = Console.ReadLine();

            Console.Write("Password => ");
            string passwords = PasswordValidation(Console.ReadLine());

            users.Add(new User(firstName, lastName, passwords));
            Console.WriteLine();

            Console.WriteLine("Data has been added");
            Console.ReadKey();

            ShowMainMenu();
        }

        private static string FirstNameValidation(string firstName)
        {
            bool status;
            do
            {
                if (firstName.Length > 0)
                {
                    status = false;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\nFirst name should not empty!");
                    Console.Write("First Name => ");
                    firstName = Console.ReadLine();
                    status = true;
                }
            }
            while (status);
            return firstName;
        }

        private static string PasswordValidation(string passwords)
        {
            bool status;
            do
            {
                if (passwords.Length > 7 && passwords.Any(new Func<char, bool>(char.IsUpper)) && passwords.Any(new Func<char, bool>(char.IsLower)) && passwords.Any(new Func<char, bool>(char.IsNumber)))
                {
                    status = false;
                }
                else
                {
                    Console.WriteLine("\nPassword must have at least 8 characters\n with at least one Capital letter, at least one lower case letter and at least one number.");
                    Console.Write("Password: ");
                    passwords = Console.ReadLine();
                    status = true;
                }
            }
            while (status);
            return passwords;
        }

        private static void EditUser()
        {
            Console.Write("Enter ID : ");
            int index = Convert.ToInt32(Console.ReadLine()) - 1;
            if (index >= 0 && index <= users.Count)
            {
                Console.Write("First Name => ");
                string fn = Console.ReadLine();
                Console.Write("Last Name => ");
                string ln = Console.ReadLine();
                Console.Write("Password => ");
                string pw = PasswordValidation(Console.ReadLine());

                users[index] = new User(fn, ln, pw);

                Console.WriteLine("User has been updated");
                Console.ReadKey();
                ShowUser();
            }
            else if (index > users.Count)
            {
                Console.WriteLine("User Not Found!");
                EditUser();
            }
            else
            {
                if (index >= 0)
                    return;
                ShowUser();
            }
        }

        private static void DeleteUser()
        {
            Console.Write("Enter ID : ");
            int select = Convert.ToInt32(Console.ReadLine()) - 1;
            if (select >= 0 && select <= users.Count)
            {
                users.RemoveAt(select);
                Console.WriteLine("Account has been deleted!");
                Console.ReadKey();
                ShowUser();
            }
            else if (select > users.Count)
            {
                Console.WriteLine("User Not Found!");
                DeleteUser();
            }
            else
            {
                if (select >= 0)
                    return;
                ShowUser();
            }
        }

        private static void ShowUser()
        {
            Console.Clear();
            int num = 1;
            Console.WriteLine("======== SHOW USER ========");
            if(users.Count == 0)
            {
                Console.WriteLine("User is empty, please add user!");
            }
            foreach(var user in users)
            {
                Console.WriteLine("========================");
                Console.WriteLine($"ID\t  : {num++}");
                Console.WriteLine($"Full Name : {user.FullName}");
                Console.WriteLine($"Username  : {user.Username}");
                Console.WriteLine($"Password  : {user.Password}");
                Console.WriteLine("========================");
            }

            Console.WriteLine("\nMenu");
            Console.WriteLine("1. Edit User");
            Console.WriteLine("2. Delete User");
            Console.WriteLine("3. Back");
            switch (Convert.ToInt32(Console.ReadLine()))
            {
                case 1:
                    EditUser();
                    break;
                case 2:
                    DeleteUser();
                    break;
                case 3:
                    ShowMainMenu();
                    break;
                default:
                    Console.WriteLine("======= Command Not Found ========");
                    ShowUser();
                    break;
            }
        }

        private static void SearchUser()
        {
            Console.Clear();
            Console.WriteLine("======== Search Account ========");
            Console.Write("Search : ");
            var search = Console.ReadLine();

            var result = users.Any(u => u.FullName.Contains(search.ToLower()));

            if (result)
            {
                foreach(var data in users)
                {
                    Console.WriteLine("================================");
                    Console.WriteLine($"Full Name : {data.FullName}");
                    Console.WriteLine($"Username  : {data.Username}");
                    Console.WriteLine($"Password  : {data.Password}");
                    Console.WriteLine("================================");
                }
            } 
            else
            {
                Console.WriteLine("User Not Found!");
            }

            Console.ReadKey();
            ShowMainMenu();
        }

        private static void Login()
        {
            Console.WriteLine("=========== Login ==============");
            Console.WriteLine("Username  : ");
            var username = Console.ReadLine();
            Console.WriteLine("Password  : ");
            var password = Console.ReadLine();

            var login = users.SingleOrDefault(u => u.Username == username && u.Password == password);

            if(login != null)
            {
                Console.WriteLine($"Login succsesful.\nHello ${login.FirstName}!");
            }
            else
            {
                Console.WriteLine("Login Failed.\nAuthentication Failed!");
            }
        }

    }
}