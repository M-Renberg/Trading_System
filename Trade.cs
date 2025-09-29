namespace App;

class Trade
{
    enum ItemStatus
    {
        None,
        Accepted,
        Pending,
        Denied,
    }

    List<Item> TradingList = new List<Item>();
    
}