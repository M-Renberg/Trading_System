using System.Collections;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text.Json;
using App;



List<User> users = new List<User>();
List<Trade> tradingList = new List<Trade>();


//List<Item> TradingListPending = new List<Item>();


//test användare

User testuser1 = new User("test", "test", "testuser");
User testuser2 = new User("arne", "arne", "arne aligator");
User testuser3 = new User("hej", "hej", "hejsan");

users.Add(testuser1);
users.Add(testuser2);
users.Add(testuser3);

//test item
testuser1.ItemList.Add(new Item("testnamn", "testtext", testuser1.Username));
testuser3.ItemList.Add(new Item("godis", "1kg", testuser3.Username));

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
             
                activeUser.AddItem();

                break;
            case "2": // se vilka Items du har lagt upp

                System.Console.WriteLine("Here are all items you have uploaded for trade:");
                activeUser.ShowOwnItem();                
                Console.ReadLine();

                break;
            case "3":  //trade system

                System.Console.WriteLine("Here are all the items avaliable for trade:");

                foreach (User user in users) //loopa igenom alla användare
                {
                    foreach (Item item in user.ItemList) //loopa igenom användares items
                    {
                        if (user != activeUser) //kolla att användaren inte är den som är inloggad för att slippa se sina egna items
                        {
                            System.Console.WriteLine($"Name: {item.Name}, Description: {item.Description}, Owner: {item.Owner}");
                        }
                    }
                }

                System.Console.WriteLine("Select the user you would like to trade with:");
                string? selectUserForTrade = Console.ReadLine(); //val av användare att byta med

                var userToTrade = users.FirstOrDefault(u => u.Username == selectUserForTrade); //gör valet till en variabel

                foreach (Item item in userToTrade.ItemList) //vissa den valda användarens items
                {
                    System.Console.WriteLine($"Name: {item.Name}, Description: {item.Description}, Owner: {item.Owner}");
                }

                System.Console.WriteLine("Write the name of the item you wish to trade");

                string? selectItem = Console.ReadLine(); //val an item

                var itemToTrade = userToTrade.ItemList.FirstOrDefault(i => i.Name == selectItem); //gör item till en variable

                System.Console.WriteLine($"You have selected {itemToTrade.Name} from user {userToTrade}"); //sanity check för att kolla att valet blev rätt

                System.Console.WriteLine(" ");


                foreach (Item item in activeUser.ItemList) //loopa fram ens egna items
                {
                    System.Console.WriteLine($"Name: {item.Name}, Description: {item.Description}");
                }

                System.Console.WriteLine("select on of your own items you wish to trade with");
                string? itemForTrade = Console.ReadLine(); //val

                var ownForTrade = activeUser.ItemList.FirstOrDefault(u => u.Name == itemForTrade); //gör till variable

                //gör om allt till strängar så jag kan lägga in det i en trade class
                string? fromusertrad = activeUser.Username;
                string? tousertrade = userToTrade.Username;
                string? toitemtrade = itemToTrade.Name;
                string? fromitemtrade = ownForTrade.Name;

                //lägg till i en trading lista
                tradingList.Add(new Trade(fromusertrad, tousertrade, fromitemtrade, toitemtrade));

                System.Console.WriteLine("trade request is now sent");
                Console.ReadLine();




                break;
            case "4": // meddelande om trade

                System.Console.WriteLine("see you pending, Accepted, trades:");
                System.Console.WriteLine("1 for pending");

                string? input_see_trade = Console.ReadLine();

                switch (input_see_trade)
                {

                    case "1":
                        System.Console.WriteLine("here are you pending trades:");

                        int tradeSerialNum = 0;

                        foreach (Trade trade in tradingList) //loopa igenom trade listan
                        {
                            if (activeUser.Username == trade.ToUser && trade.Status == Trade.TradeStatus.Pending) //kolla så att det är trades som är skicka till en och att dom är pending
                            {
                                System.Console.WriteLine($"Trade nr: {tradeSerialNum} User: {trade.FromUser} wish to trade you item {trade.ToUserItem} for {trade.FromUserItem}");
                            }

                            tradeSerialNum++;
                        }

                        System.Console.WriteLine("select trade nr to accept or deny trade");

                        string? input_pending = Console.ReadLine();
                        int.TryParse(input_pending, out int input_pending_int);

                        foreach (Trade trade in tradingList) //loopa igenom trade listan
                        {
                            if (activeUser.Username == trade.ToUser && trade.Status == Trade.TradeStatus.Pending) //kolla så att det är trades som är skicka till en och att dom är pending
                            {
                                if (input_pending_int == tradeSerialNum)
                                {
                                    System.Console.WriteLine($"Trade nr: {tradeSerialNum} User: {trade.FromUser} wish to trade you item {trade.ToUserItem} for {trade.FromUserItem}");
                                    System.Console.WriteLine("Will you accept or deny this trade?");
                                    System.Console.WriteLine("1. for accept");
                                    System.Console.WriteLine("2. for deny");
                                    string? input_acceptdeny = Console.ReadLine();

                                    if (input_acceptdeny == "1")
                                    {
                                        System.Console.WriteLine("you have accpted the rade");
                                        trade.Status = Trade.TradeStatus.Accepted;
                                    }
                                    else if (input_acceptdeny == "2")
                                    {
                                        System.Console.WriteLine("you have denied the trade");
                                        trade.Status = Trade.TradeStatus.Denied;
                                    }

                                }
                                tradeSerialNum++;
                            }
                        }

                        Console.ReadLine();


                        break;

                    case "2": //accpted trades

                     foreach (Trade trade in tradingList) //loopa igenom trade listan
                        {
                            if (activeUser.Username == trade.ToUser && trade.Status == Trade.TradeStatus.Accepted) //kolla så att det är trades som är skicka till en och att dom är pending
                            {
                                System.Console.WriteLine($"Accepted trade from User: {trade.FromUser} wish to trade you item {trade.ToUserItem} for {trade.FromUserItem}");
                            }
                        }
                        break;

                    case "3": //denied trades
                    
                        foreach (Trade trade in tradingList) //loopa igenom trade listan
                        {
                            if (activeUser.Username == trade.ToUser && trade.Status == Trade.TradeStatus.Denied) //kolla så att det är trades som är skicka till en och att dom är pending
                            {
                                System.Console.WriteLine($"Denied trade from User: {trade.FromUser} wish to trade you item {trade.ToUserItem} for {trade.FromUserItem}");
                            }
                        }
                        break;
                    case "4":
                        break;



                }



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
