using System.Text.Json; //så att jag kan använda json
namespace App;

class Datamanager //save and load class
{

    public string dataDir; //string för mapp
    public string userData; //sting för .json fil
    public string tradeData; //sting för .json fil

    public Datamanager() //konstruktion
    {
        dataDir = Path.Combine(Directory.GetCurrentDirectory(), "data"); //så den hittar rätt mapp
        userData = Path.Combine(dataDir, "Users.json"); //lägger ihop mapp och fil
        tradeData = Path.Combine(dataDir, "Trades.json"); //lägger ihop mapp och fil

    }

public void LoadUser(List<User> users) //metod för att ladda in sparade användare i listan
    {
        if (File.Exists(userData)) //kolla så att filen finns
        {
            string loadUsers = File.ReadAllText(userData); //skapar en variabel som läser all text i json filen
            if (!string.IsNullOrWhiteSpace(loadUsers)) //kollar variabel
            { 
                var loadedUsers = JsonSerializer.Deserialize<List<User>>(loadUsers, new JsonSerializerOptions { IncludeFields = true }); //ny variabel för att göra json listan
                if (loadedUsers != null) //kollar så den inte är null
                {
                    users.Clear(); //ränsar om det finns något i listan
                    users.AddRange(loadedUsers); //laddar in det sparade i listan
                }
            }
            
        }
    }

    public void LoadTrade(List<Trade> tradingList) //metod för att ladda in sparade tradaes i listan
    {
        if (File.Exists(tradeData)) //kolla så att filen finns
        {
            string loadTrades = File.ReadAllText(tradeData); //skapar en variabel som läser in att data från json filen.
            if (!string.IsNullOrWhiteSpace(loadTrades)) //kollar variabel
            {
                var loadedTrades = JsonSerializer.Deserialize<List<Trade>>(loadTrades, new JsonSerializerOptions { IncludeFields = true }); //ny variabel för att göra json listan
                if (loadedTrades != null) // kollar den är null
                {
                    tradingList.Clear(); //tömmer listan
                    tradingList.AddRange(loadedTrades); //laddar inte det sparade i listan
                }
            }
                
        }
    }

    public void SaveUser(List<User> users) //metod för att spara användare
    {
        string saveUsers = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true, IncludeFields = true }); //gör en sträng som json kan använda för att spara ner
        File.WriteAllText(userData, saveUsers); //spara det i json
    }
    public void SaveTrade(List<Trade> tradingList) //metod för att spara trades
    {
        string saveTrades = JsonSerializer.Serialize(tradingList, new JsonSerializerOptions { WriteIndented = true, IncludeFields = true }); //gör en sträng som json kan använda för att spara ner
        File.WriteAllText(tradeData, saveTrades); //spara den i json
    }

}


