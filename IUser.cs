namespace App;

interface IUser
{
    // använde detta sett som inloggning då vi gjorde samma sak i "school" projektet
    public bool TryLogin(string email, string password);

    void AddItem();
    void ShowOwnItem();
}