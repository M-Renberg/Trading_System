namespace App;

class User
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
}