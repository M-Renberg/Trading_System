namespace App;

class Item
{
    public string Name;
    public string Description;
    public string Owner; // vill kunna lägga till med som ägare till produkten man ska byta




    public Item(string name, string description, string owner)
    {
        Name = name;
        Description = description;
        Owner = owner;
    }


}