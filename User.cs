using System.Buffers;

namespace App;

class User
{

    public string Username; // använder detta som det id i trades
    public string Email; //för inloggning
    public string _password;   //för inloggning
    public List<Item> ItemList = new List<Item>(); //item list

    public User() { } //till json så att det sparas ner rätt 

    public User(string email, string password, string username) //konstruktor
    {
        Email = email;
        _password = password;
        Username = username;
    }

    public bool TryLogin(string email, string password) //login funktion
    {
        return email == Email && password == _password; //kollar så att email och lösenord stämmer med det man skickar in.
    }

    public void AddItem() // lägga till item för trade
    {

        System.Console.WriteLine("Add Item for trade");
        System.Console.WriteLine("Please enter the name of the item:");
        string? itemName = Console.ReadLine(); //name på item
        System.Console.WriteLine("Please enter a short description of the item:");
        string? itemDescription = Console.ReadLine();// beskriving av item
        System.Console.WriteLine($"You have added item {itemName}");
        System.Console.WriteLine("With description:");
        System.Console.WriteLine(itemDescription);

        System.Console.WriteLine($"You have added item with name {itemName} and description: {itemDescription}");
        // System.Console.WriteLine("Are you satisfied or would you like to change something?");

        Console.ReadLine();

        string? Owner = Username; // ser till att det blir rätt ägare

        ItemList.Add(new Item(itemName!, itemDescription!, Owner!)); //lägg till item i lista

    }

    public void ShowOwnItem() //loopar fram ens items 
    {
        foreach (Item i in ItemList) //foreach loop för att loopa igen listan med items
        {

            System.Console.WriteLine($"Name: {i.Name}, Description: {i.Description}, Owner:{i.Owner}"); //skriv ut

        }
    }
    
    
    


}