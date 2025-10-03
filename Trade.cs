namespace App;

class Trade
{
    public enum TradeStatus //enum för att kunna hålla koll på statusen i en trade
    {
        None, //la till none för att på genomgången av det sades det att man skulle börja med det för att ha en default typ
        Accepted,
        Pending,
        Denied,
    }



    public string FromUser; //vem traden är från
    public string ToUser; //vem som blir frågad om en trade
    public string FromUserItem; //vilket item den som frågar vill byta bort
    public string ToUserItem; //vilket item den som frågar vill ha

    public TradeStatus Status; //för att använda en status i trade konstruktorn



    public Trade(string fromuser, string touser, string fromuseritem, string touseritem) //konstruktor
    {
        FromUser = fromuser;
        ToUser = touser;
        FromUserItem = fromuseritem;
        ToUserItem = touseritem;

        Status = TradeStatus.Pending; //default att den är pending


    }


    public static void PendingTrade(List<Trade> tradingList, User? activeUser) //jag ville bara testa göra detta till en static metod, och det funka.
    {

        Datamanager dm = new Datamanager(); //för att kunna använda save metoder
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

        tradeSerialNum = 0; //reset så att den kan återanvändas och gemföras med TryParse senare

        int.TryParse(input_pending, out int input_pending_int); //gör om input till int


        foreach (Trade trade in tradingList) //loopa igenom trade listan
        {
            if (activeUser.Username == trade.ToUser && trade.Status == Trade.TradeStatus.Pending) //kolla så att det är trades som är skicka till en och att dom är pending
            {
                if (input_pending_int == tradeSerialNum) // om input stämmer med vilket item man har valt
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
                        dm.SaveTrade(tradingList); //spara bytet med ny status
                                                   //kommentar till mig själv!!!! lägga till så att man tar bort från båda users item listor??????

                    }
                    else if (input_acceptdeny == "2") //deny
                    {
                        System.Console.WriteLine("you have denied the trade");
                        trade.Status = Trade.TradeStatus.Denied; //ändra enum
                        dm.SaveTrade(tradingList); //spara bytet med ny status
                    }

                }
                tradeSerialNum++; // plusar på i varje loop omgång tills man hittar rätt.
            }
        }
        Console.ReadLine();

    }

    public static void AcceptedTrade(List<Trade> tradingList, User? activeUser)
    {
        
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

    }


    public static void DeniedTrade(List<Trade> tradingList, User? activeUser)
    {
        
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
        
    }
}