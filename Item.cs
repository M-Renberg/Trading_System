namespace App;

class Item //item klass
{
    public string Name; //namn på itemet
    public string Description; //beskrivning
    public string Owner; // vill kunna lägga till med som ägare till produkten man ska byta




    public Item(string name, string description, string owner) //konstruktor
    {
        Name = name;
        Description = description;
        Owner = owner;
    }


}