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

}