using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.Json;
using App;

//string userDataFile = "./data/Userdate.json";

List<User> users = new List<User>();

//string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
//File.AppendAllText(userDataFile, json);
//test användare

User testuser1 = new User("test", "test", "testuser");
User testuser2 = new User("arne", "arne", "arne aligator");

users.Add(testuser1);
users.Add(testuser2);

//test item
testuser1.ItemList.Add(new Item("testnamn", "testtext", "testuser"));

//var itemId = item.ElementAt(0);
User? activeUser = null; //ser till att användare är null(inte finns)

bool running = true;

while (running)
{

    if (activeUser == null) //om activeUser är null körs inloggningen
    {

        try { Console.Clear(); } catch { } //console.clear work around. tagit i från bloggen för att inte crasha debug.
    
        Console.WriteLine("Welcome to The Trading System"); //meny
        System.Console.WriteLine(" "); //gillar att det blir ett hopp i texten
        System.Console.WriteLine("1. To login.");
        System.Console.WriteLine("2. Create new user.");
        System.Console.WriteLine("3. To exit software.");

        string? input = Console.ReadLine(); //meny input

        Debug.Assert(input != null);

        switch (input) //meny funktioner 
        {
            case "1": //log in funktion
                System.Console.WriteLine("The Trading System login");
                System.Console.WriteLine("Please write your email:");
                string? inputEmail = Console.ReadLine();
                System.Console.WriteLine("Please write your password:");
                string? inputPassword = Console.ReadLine();

                foreach (User user in users) //letar och gemför sparade användare
                {
                    if (user.TryLogin(inputEmail!, inputPassword!))
                    {
                        activeUser = user; //byta activeUser från null till den som loggar in
                        break;
                    }
                }
                break;

            case "2": //skapa ny användare
                System.Console.WriteLine("The Trading System create account");
                System.Console.WriteLine("Please write the email you would like to use:");
                string? inputNewEmail = Console.ReadLine(); // inloggnings mail
                System.Console.WriteLine("Please enter the password you would like to use:");
                string? inputNewPassword = Console.ReadLine(); // inloggnings lösenord
                System.Console.WriteLine("Please enter the username you would like to display:");
                string? inputNewUsername = Console.ReadLine(); //detta ingår inte i uppgiften men jag ville ha ett anvädarnamn för jag tycker det blir tydligen vem som är användare. återanvänder detta även när man ska se vem som äger ett item och i bytes funktionen.

                users.Add(new User(inputNewEmail!, inputNewPassword!, inputNewUsername!)); //lägger till i lista

                System.Console.WriteLine("A new user has been created");
                System.Console.WriteLine($"You can now login as {inputNewUsername}"); //bara extra så det ser snyggare ut, samt sanity check
                Console.ReadLine();
                break;

            case "3": // stänga programmet
                running = false;
                break;
            default: //vid felaktig inmatning i menyn
                System.Console.WriteLine("Something went wrong. Please try again.");
                break;
        }
    }
    else // menyn när man är inloggad
    {   
        try{Console.Clear();}catch{} 
        System.Console.WriteLine("Welcome to The Trading System");
        System.Console.WriteLine($"You're logged in as {activeUser}"); //kolla vem som är inloggad
        System.Console.WriteLine(" ");
        System.Console.WriteLine("1. Add an item for trade.");
        System.Console.WriteLine("2. show your own items");
        System.Console.WriteLine("3. Browes items for trade");
        System.Console.WriteLine("4. Trading inbox");
        System.Console.WriteLine("9. logout");

        string? input = Console.ReadLine();
        switch (input)
        {
            case "1": //lägga till nytt item för trade
             
                activeUser.AddItem(); //1061????????????? googla fram att jag kunde inte använda user.additem för fick fel meddelande. när jag testa activeUser fick jag också det. fick fram att jag kunde lägga till metodernas namn i IUser för att kunna kalla på dom som den inloggade användaren.

                break;
            case "2": // se vilka Items du har lagt upp
                System.Console.WriteLine("Here are all items you have uploaded for trade:");

                activeUser.ShowOwnItem();

                
                Console.ReadLine();

                break;
            case "3":  //leta efter items

                System.Console.WriteLine("Here are all the items that are available for trade:");
                System.Console.WriteLine(" ");


                int selectId = 0; //för att man ska se på vilken plats i item listan något ligger

                foreach (User user in users)
                {
                    foreach (Item item in user.ItemList)
                    {
                        System.Console.WriteLine($"Item id: {selectId}, Name: {item.Name}, Description: {item.Description}, Owner: {item.Owner}");

                    }

                    System.Console.WriteLine("write the Owner you wish to trade with:");
                    string? input_trade = Console.ReadLine();

                    if (input_trade == user.Username)
                    {

                        System.Console.WriteLine($"You have selected: {user.Username}");
                        System.Console.WriteLine("select the item id you want to trade:");
                        string? input_trade_id = Console.ReadLine();
                        int.TryParse(input_trade_id, out int input_trade_id_int);

                        System.Console.WriteLine($"you wish to trade with {user.Username} and item {user.ItemList[input_trade_id_int].Name}");

                        System.Console.WriteLine("select one of ypur items you wish to trade with:");
                        activeUser.ShowOwnItem();


                    }

                }

                

                Console.ReadLine();

                break;
            case "4": // meddelande om trade
                System.Console.WriteLine("");

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
