namespace App;

class User : IUser
{

    public string Username; // name som vissas 
    public string Email; //för inloggning
    string _password;   //för inloggning

    public User(string email, string password, string username)
    {
        Email = email;
        _password = password;
        Username = username;
    }

    public bool TryLogin(string email, string password)
    {
        return email == Email && password == _password;
    }

    public override string ToString() //test för att försöka displaya användarnamn
    {
        return Username;
    }

}