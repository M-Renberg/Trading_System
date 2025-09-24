using App;

List<IUser> users = new List<IUser>();
//test användare
users.Add(new User("test", "test", "testuser"));

IUser? activeUser = null; //ser till att användare är null(inte finns)

bool running = true;

while (running)
{

    if (activeUser == null) //om användare är null körs inloggningen
    {
        Console.WriteLine("Welcome to The Trading System");
        System.Console.WriteLine(" ");
        System.Console.WriteLine("1. To login.");
        System.Console.WriteLine("2. Create new user.");
        System.Console.WriteLine("3. To exit software.");

        string? input = Console.ReadLine();

        switch (input)
        {
            case "1":
                System.Console.WriteLine("The Trading System login");
                System.Console.WriteLine("Please write your email:");
                string? inputEmail = Console.ReadLine();
                System.Console.WriteLine("Please write your password:");
                string? inputPassword = Console.ReadLine();

                foreach (IUser user in users)
                {
                    if (user.TryLogin(inputEmail, inputPassword))
                    {
                        activeUser = user;
                        break;
                    }
                }
                break;

            case "2":
                System.Console.WriteLine("The Trading System create account");
                System.Console.WriteLine("Please write the email you would like to use:");
                string? inputNewEmail = Console.ReadLine();
                System.Console.WriteLine("Please enter the password you would like to use:");
                string? inputNewPassword = Console.ReadLine();
                System.Console.WriteLine("Please enter the username you would like to display:");
                string? inputNewUsername = Console.ReadLine();
                break;

            case "3":
                break;
        }
    }
    else
    {
        System.Console.WriteLine("Welcome to The Trading System");
        System.Console.WriteLine($"You're logged in as {activeUser}");

        string? input = Console.ReadLine();
        switch (input)
        {
            case "1":
                break;
            case "2":
                break;
            case "9":
                activeUser = null;
                break;
        }
    }
    
    
}
