namespace App;

class Item
{
    public string Name;
    public string Description;
    public string Owner; // vill kunna lägga till med som ägare till produkten man ska byta

    // List<Item> items = new List<Item>();


    public Item(string name, string description, string owner)
    {
        Name = name;
        Description = description;
        Owner = owner;
    }

    // public void showItems() //detta blev kaos.... 
    // {

    // }

    // public void addItem(string itemName,string itemDescription, string itemOwner)
    // {
    //     //item.Add(new Item(itemName, itemDescription, activeUser.ToString());
    // }
}