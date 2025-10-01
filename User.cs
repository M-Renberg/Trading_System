using System.Buffers;

namespace App;

class User //: IUser
{

    public string Username; // name som vissas 
    public string Email; //för inloggning
    public string _password;   //för inloggning
    public List<Item> ItemList = new List<Item>();

    public User () {}
    public User(string email, string password, string username)
    {
        Email = email;
        _password = password;
        Username = username;
    }

    public bool TryLogin(string email, string password)
    {
        return email == Email && password == _password;
    }


    public override string ToString() //test för att försöka displaya användarnamn
    {
        return Username;
    }

    public void AddItem()
    {

        System.Console.WriteLine("Add Item for trade");
        System.Console.WriteLine("Please enter the name of the item:");
        string? itemName = Console.ReadLine(); //name på item
        System.Console.WriteLine("Please enter a short description of the item:");
        string? itemDescription = Console.ReadLine();// beskriving av item
        System.Console.WriteLine($"You have added item {itemName}");
        System.Console.WriteLine("With description:");
        System.Console.WriteLine(itemDescription);

        System.Console.WriteLine($"You want to add item {itemName} with description: {itemDescription}");
        System.Console.WriteLine("Are you satisfied or would you like to change something?");

        string? itemAddChoice = Console.ReadLine();

        string? Owner = Username;

        ItemList.Add(new Item(itemName!, itemDescription!, Owner!));

    }

    public void ShowOwnItem()
    {
        int itemID = 0;
        foreach (Item i in ItemList) //foreach loop för att loopa igen listan med items
        {

            System.Console.WriteLine($"Item id: {itemID}, Name: {i.Name}, Description: {i.Description}, Owner:{i.Owner}");

        }
    }
    


}