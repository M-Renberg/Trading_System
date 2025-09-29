using System.Diagnostics;
using System.Runtime.InteropServices;
using App;



List<IUser> users = new List<IUser>();
//test användare

User testuser1 = new User("test", "test", "testuser");
User testuser2 = new User("arne", "arne", "arne aligator");

users.Add(testuser1);
users.Add(testuser2);

//List<Item> item = new List<Item>();
//test item
//item.Add(new Item("testnamn", "testtext", "testuser"));
testuser1.ItemList.Add(new Item("testnamn", "testtext", "testuser"));

//var itemId = item.ElementAt(0);
IUser? activeUser = null; //ser till att användare är null(inte finns)

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

                foreach (IUser user in users) //letar och gemför sparade användare
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
                      //     while (addingRun)
                      //     {

                //     System.Console.WriteLine("Add Item for trade");
                //     System.Console.WriteLine("Please enter the name of the item:");
                //     string? itemName = Console.ReadLine(); //name på item
                //     System.Console.WriteLine("Please enter a short description of the item:");
                //     string? itemDescription = Console.ReadLine();// beskriving av item
                //     System.Console.WriteLine($"You have added item {itemName}");
                //     System.Console.WriteLine("With description:");
                //     System.Console.WriteLine(itemDescription);

                //     System.Console.WriteLine($"You want to add item {itemName} with description: {itemDescription}");
                //     System.Console.WriteLine("Are you satisfied or would you like to change something?");
                //     System.Console.WriteLine("1. Make no changes and upload item");
                //     System.Console.WriteLine("2. Make changes");
                //     string? itemAddChoice = Console.ReadLine();

                //     if (itemAddChoice == "1")
                //     {
                //         User.Itemlist.Add(new Item(itemName!, itemDescription!, activeUser.ToString()!));

                //         break;
                //     }

                //     else if (itemAddChoice == "2")
                //     {
                //         continue;
                //     }
                // }


                activeUser.AddItem(); //1061?????????????

                break;
            case "2": // se vilka Items du har lagt upp
                System.Console.WriteLine("Here are all items you have uploaded for trade:");

                // if (activeUser is User userItems) //kollar så att activeUser är en User och ger mig en variabel att jobba med
                // {
                //     foreach (Item i in item) //foreach loop för att loopa igen listan med items
                //     {
                //         if (userItems.Username == i.Owner) //kollar på att användaren har samma namn som ägaren av ett item
                //         {
                //             System.Console.WriteLine($"{i.Name}, {i.Description}, {i.Owner}");
                //         }
                //     }
                // }
                activeUser.ShowOwnItem();

                
                Console.ReadLine();

                break;
            case "3":  //leta efter items

                System.Console.WriteLine("Here are all the items that are available for trade:");
                System.Console.WriteLine(" ");


                int selectId = 0; //för att man ska se på vilken plats i item listan något ligger
                foreach (Item i in item) //loop fram listan
                {

                    System.Console.WriteLine($"Item id: {selectId}, Name: {i.Name}, Description: {i.Description}, Owner: {i.Owner}");
                    selectId++; // plus för varje gång loopen går
                }

                System.Console.WriteLine(" ");
                System.Console.WriteLine("Select item id of the item you would like to trade:");
                string? selectInput = Console.ReadLine(); //input som ska användas till att list plats

                int.TryParse(selectInput, out int selectInput_id); //gör om input till en int
        

                Console.WriteLine("You have selected:");
                Console.WriteLine($"{item[selectInput_id].Name}, {item[selectInput_id].Description}, {item[selectInput_id].Owner}");
                Console.ReadLine();

                System.Console.WriteLine("select the item id of you own items you wish to trade with:");
                string? selectInput_two = Console.ReadLine();
                int.TryParse(selectInput_two, out int selectInput_id_two);
                    
                    
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
