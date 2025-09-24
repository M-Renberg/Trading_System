using App;

List<IUser> users = new List<IUser>();
//test användare
users.Add(new User("test", "test", "testuser"));

List<Item> item = new List<Item>();
//test item
item.Add(new Item("testnamn", "testtext"));

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

                foreach (IUser user in users) //login funktion
                {
                    if (user.TryLogin(inputEmail, inputPassword))
                    {
                        activeUser = user;
                        break;
                    }
                }
                // System.Console.WriteLine("logged in " + (activeUser != null)); //bara för att testa log in
                // Console.ReadLine();
                break;

            case "2": //skapa ny användare
                System.Console.WriteLine("The Trading System create account");
                System.Console.WriteLine("Please write the email you would like to use:");
                string? inputNewEmail = Console.ReadLine();
                System.Console.WriteLine("Please enter the password you would like to use:");
                string? inputNewPassword = Console.ReadLine();
                System.Console.WriteLine("Please enter the username you would like to display:");
                string? inputNewUsername = Console.ReadLine();

                users.Add(new User(inputNewEmail, inputNewPassword, inputNewUsername)); //lägger till i lista

                System.Console.WriteLine("A new user has been created");
                System.Console.WriteLine($"You can now login as {inputNewUsername}"); //bara extra så det ser snyggare ut
                Console.ReadLine();
                break;

            case "3": // stränga programmet
                running = false;
                break;
            default:
                System.Console.WriteLine("Something went wrong. Please try again.");
                break;
        }
    }
    else
    {
        System.Console.WriteLine("Welcome to The Trading System");
        System.Console.WriteLine($"You're logged in as {activeUser}"); //kolla vem som är inloggad

        string? input = Console.ReadLine();
        switch (input)
        {
            case "1": //lägga till nytt item för trade
                break;
            case "2": // se vilka Items du har lagt upp
                break;
            case "3": // meddelande om trade
                break;
            case "4": //leta efter items
                break;
            case "9": //logga ut
                activeUser = null;
                break;
            case "quit": // extra för att stänga helt programmet
                running = false;
                break;
            default:
                System.Console.WriteLine("Something went wrong. Please try again.");
                break;
        }
    }
    
    
}
