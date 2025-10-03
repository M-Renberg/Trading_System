using System.Collections;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text.Json;
using App;

Datamanager dm = new Datamanager(); //anropa Datamanager klassen

List<User> users = new List<User>(); //användar lista
dm.LoadUser(users); //ladda in från json fil till listan

List<Trade> tradingList = new List<Trade>(); //trade lista
dm.LoadTrade(tradingList); //ladda in från json fil till listan

User? activeUser = null; //ser till att användare är null(inte finns)

bool running = true; //bool för while loop

while (running) //while loop som kör programmet
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

        Debug.Assert(input != null); //en debug jag lät vara kvar för att vissa att jag använt det typ

        switch (input) //meny funktioner 
        {
            case "1": //login funktion
                System.Console.WriteLine("The Trading System login");
                System.Console.WriteLine("Please write your email:");
                string? inputEmail = Console.ReadLine();
                System.Console.WriteLine("Please write your password:");
                string? inputPassword = Console.ReadLine();

                foreach (User user in users) //letar och gemför sparade användare
                {
                    if (user.TryLogin(inputEmail!, inputPassword!)) //gemför email och lösenord
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
                if (inputNewEmail == null) //så att man inte kan ha null som användare
                {
                    System.Console.WriteLine("Something went wrong");
                    Console.ReadLine();
                    break;
                }
                System.Console.WriteLine("Please enter the password you would like to use:");
                string? inputNewPassword = Console.ReadLine(); // inloggnings lösenord
                if (inputNewPassword == null) //så att man inte kan ha null som lösenord
                {
                    System.Console.WriteLine("Something went wrong");
                    Console.ReadLine();
                    break;
                }
                System.Console.WriteLine("Please enter the username you would like to display:");
                string? inputNewUsername = Console.ReadLine(); //detta ingår inte i uppgiften men jag ville ha ett anvädarnamn för jag tycker det blir tydligen vem som är användare. återanvänder detta även när man ska se vem som äger ett item och i bytes funktionen.
                if (inputNewUsername == null) //så att man inte kan ha null som användarnamn
                {
                    System.Console.WriteLine("Something went wrong");
                    Console.ReadLine();
                    break;
                }

                users.Add(new User(inputNewEmail!, inputNewPassword!, inputNewUsername!)); //lägger till i lista
                dm.SaveUser(users); //spara användare

                System.Console.WriteLine("A new user has been created");
                System.Console.WriteLine($"You can now login as {inputNewUsername}"); //bara extra så det ser snyggare ut, samt sanity check
                Console.ReadLine();
                break;

            case "3": // stänga programmet
                dm.SaveUser(users); //sparar användare
                running = false; //dödar loopen
                break;

            default: //vid felaktig inmatning i menyn
                System.Console.WriteLine("Something went wrong. Please try again.");
                break;
        }
    }
    else // menyn när man är inloggad
    {
        try { Console.Clear(); } catch { }

        System.Console.WriteLine("Welcome to The Trading System"); //meny text
        System.Console.WriteLine($"You're logged in as {activeUser.Username}"); //kolla vem som är inloggad. det var en sanity check för mig
        System.Console.WriteLine(" ");
        System.Console.WriteLine("1. Add an item for trade.");
        System.Console.WriteLine("2. show your own items");
        System.Console.WriteLine("3. Browes items for trade");
        System.Console.WriteLine("4. Trading inbox");
        System.Console.WriteLine("9. logout");

        string? input = Console.ReadLine();
        switch (input) //meny
        {
            case "1": //lägga till nytt item för trade
                activeUser.AddItem(); //lägga till item
                dm.SaveUser(users); //spara användare och item som är inne i user klassen             
            break;

            case "2": // se vilka Items du har lagt upp
                System.Console.WriteLine("Here are all items you have uploaded for trade:");
                activeUser.ShowOwnItem(); //loopa fram ens items
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

                User? userToTrade = null; //variabel för user man vill byta med
                
                foreach (User user in users) //loopa users
                {
                    if (user.Username == selectUserForTrade) //om användarnamnet stämmer
                    {
                        userToTrade = user; //gör variabel till den User
                        break;
                    }
                }

                foreach (Item item in userToTrade!.ItemList) //vissa den valda användarens items
                {
                    System.Console.WriteLine($"Name: {item.Name}, Description: {item.Description}, Owner: {item.Owner}");
                }
                System.Console.WriteLine("Write the name of the item you wish to trade");
                string? selectItem = Console.ReadLine(); //val an item
                
                Item? itemToTrade = null; //variabel item man vill ha från bytet

                foreach (Item item in userToTrade.ItemList) //loop from item från users lista
                {
                    if (item.Name == selectItem) //kolla så namnet är rätt
                    {
                        itemToTrade = item; //gör variabel till det namnet
                        break;
                    }
                }

                System.Console.WriteLine($"You have selected {itemToTrade!.Name} from user {userToTrade}"); //sanity check för att kolla att valet blev rätt
                System.Console.WriteLine(" ");
                foreach (Item item in activeUser.ItemList) //loopa fram ens egna items
                {
                    System.Console.WriteLine($"Name: {item.Name}, Description: {item.Description}");
                }

                System.Console.WriteLine("select on of your own items you wish to trade with");
                string? itemForTrade = Console.ReadLine(); //val
                var ownForTrade = activeUser.ItemList.FirstOrDefault(u => u.Name == itemForTrade); //gör till variable
                //gör om allt till strängar så jag kan lägga in det i en trade class
                string? fromusertrad = activeUser.Username; //gör om mina variablar till strings så att jag kan lägga in dom i en trade class
                string? tousertrade = userToTrade.Username;
                string? toitemtrade = itemToTrade.Name;
                string? fromitemtrade = ownForTrade!.Name;
                //lägg till i en trading lista

                tradingList.Add(new Trade(fromusertrad, tousertrade, fromitemtrade, toitemtrade)); //skapa trade classen
                dm.SaveTrade(tradingList);//spara trade

                System.Console.WriteLine("trade request is now sent"); //sanity check
                Console.ReadLine();
                break;

            case "4": // meddelande om trade

                System.Console.WriteLine("see you pending, Accepted, trades:");
                System.Console.WriteLine("1 for pending");
                System.Console.WriteLine("2. for Accepted");
                System.Console.WriteLine("3. for Denied ");
                System.Console.WriteLine("4. back");

                string? input_see_trade = Console.ReadLine();
                switch (input_see_trade)
                {
                    case "1": //pending
                        System.Console.WriteLine("here are you pending trades:");

                        int tradeSerialNum = 0; //fake id

                        foreach (Trade trade in tradingList) //loopa igenom trade listan
                        {
                            if (activeUser.Username == trade.ToUser && trade.Status == Trade.TradeStatus.Pending) //kolla så att det är trades som är skicka till en och att dom är pending
                            {
                                System.Console.WriteLine($"Trade nr: {tradeSerialNum} User: {trade.FromUser} wish to trade you item {trade.ToUserItem} for {trade.FromUserItem}");
                                tradeSerialNum++; //plus på fake id efter att en rad skrivits ut

                            }
                        }

                        System.Console.WriteLine("select trade nr to accept or deny trade");
                        string? input_pending = Console.ReadLine();

                        tradeSerialNum = 0; //gjorde en ny in för att slippa skriva logik att calibrera om den andra.

                        int.TryParse(input_pending, out int input_pending_int); //gör om input till int


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

                                    if (input_acceptdeny == "1") //accept
                                    {
                                        System.Console.WriteLine("you have accpted the trade");
                                        trade.Status = Trade.TradeStatus.Accepted; //ändra enum
                                        dm.SaveTrade(tradingList);
                                        //kommentar till mig själv!!!! lägga till så att man tar bort från båda users item listor??????

                                    }
                                    else if (input_acceptdeny == "2") //deny
                                    {
                                        System.Console.WriteLine("you have denied the trade");
                                        trade.Status = Trade.TradeStatus.Denied; //ändra enum
                                        dm.SaveTrade(tradingList);
                                    }

                                }
                                tradeSerialNum++; //kontrollera fram till man hittar rätt id
                            }
                        }
                        Console.ReadLine();
                        break;

                    case "2": //accpted trades

                        System.Console.WriteLine("Accepted Trades:");
                        System.Console.WriteLine(" ");
                        foreach (Trade trade in tradingList) //loopa igenom trade listan
                        {
                            if (activeUser.Username == trade.ToUser && trade.Status == Trade.TradeStatus.Accepted) //kolla så att det är trades som är skicka till en och att dom är pending
                            {

                                System.Console.WriteLine($"Accepted trade from User: {trade.FromUser} wish to trade you item {trade.ToUserItem} for {trade.FromUserItem}");
                                System.Console.WriteLine($"Please contact {trade.FromUser} to arrange a pick up/ trade point");
                            }
                        }
                        Console.ReadLine();
                        break;

                    case "3": //denied trades

                        System.Console.WriteLine("Declined Trades:");
                        System.Console.WriteLine(" ");

                        foreach (Trade trade in tradingList) //loopa igenom trade listan
                        {
                            if (activeUser.Username == trade.ToUser && trade.Status == Trade.TradeStatus.Denied) //kolla så att det är trades som är skicka till en och att dom är pending
                            {
                                System.Console.WriteLine($"Denied trade from User: {trade.FromUser} wish to trade you item {trade.ToUserItem} for {trade.FromUserItem}");
                            }
                        }
                        Console.ReadLine();
                        break;
                    case "4": //back
                        break;
                    default:
                        System.Console.WriteLine("Something went wrong. Please try again.");
                    break;
                }
                break;
            case "9": //logga ut
                dm.SaveUser(users); //spara användare 
                activeUser = null;
                break;
            case "quit": // extra för att stänga helt programmet
                dm.SaveUser(users); //spara användare
                running = false;
                break;
            default:
                System.Console.WriteLine("Something went wrong. Please try again.");
                break;
        }
    }
}
