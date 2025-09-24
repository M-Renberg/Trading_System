namespace App;

class User : IUser
{

    public string Username; // name som vissas 
    public string Email; //för inloggning
    string _password;   //för inloggning

    public User(string username, string email, string password)
    {
        Username = username;
        Email = email;
        _password = password;
    }

    public bool TryLogin(string email, string password)
    {
        return email == Email && password == _password;
    }
}