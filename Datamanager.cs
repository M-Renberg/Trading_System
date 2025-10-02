using System.Text.Json;
namespace App;

class Datamanager
{

    public string dataDir;
    public string userData;
    public string tradeData;

    public Datamanager()
    {
        dataDir = Path.Combine(Directory.GetCurrentDirectory(), "data");
        userData = Path.Combine(dataDir, "Users.json");
        tradeData = Path.Combine(dataDir, "Trades.json");

    }

public void LoadUser(List<User> users)
    {
        if (File.Exists(userData)) // hämta användare från users.json
        {
            string loadUsers = File.ReadAllText(userData);
            if (!string.IsNullOrWhiteSpace(loadUsers))
            { 
                var loadedUsers = JsonSerializer.Deserialize<List<User>>(loadUsers, new JsonSerializerOptions { IncludeFields = true });
                if (loadedUsers != null)
                {
                    users.Clear();
                    users.AddRange(loadedUsers);
                }
            }
            
        }
    }

    public void LoadTrade(List<Trade> tradingList)
    {
        if (File.Exists(tradeData)) //hämta trade från trades.json
        {
            string loadTrades = File.ReadAllText(tradeData);
            if (!string.IsNullOrWhiteSpace(loadTrades))
            {
                var loadedTrades = JsonSerializer.Deserialize<List<Trade>>(loadTrades, new JsonSerializerOptions { IncludeFields = true });
                if (loadedTrades != null)
                {
                    tradingList.Clear();
                    tradingList.AddRange(loadedTrades);
                }
            }
                
        }
    }

    public void SaveUser(List<User> users)
    {
        string saveUsers = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true, IncludeFields = true });
        File.WriteAllText(userData, saveUsers);
    }
    public void SaveTrade(List<Trade> tradingList)
    {
        string saveTrades = JsonSerializer.Serialize(tradingList, new JsonSerializerOptions { WriteIndented = true, IncludeFields = true });
        File.WriteAllText(tradeData, saveTrades);
    }

}


