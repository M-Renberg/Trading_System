namespace App;

class Trade
{
    public enum TradeStatus
    {
        None,
        Accepted,
        Pending,
        Denied,
    }



    public string FromUser;
    public string ToUser;
    public string FromUserItem;
    public string ToUserItem;

    public TradeStatus Status;


    public Trade(string fromuser, string touser, string fromuseritem, string touseritem)
    {
        FromUser = fromuser;
        ToUser = touser;
        FromUserItem = fromuseritem;
        ToUserItem = touseritem;

        Status = TradeStatus.Pending;


    }

}