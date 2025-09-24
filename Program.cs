using App;


//test användare
User testuser = new User("test", "test", "testuser");

bool running = true;

while (running)
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
