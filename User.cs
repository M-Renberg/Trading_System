namespace App;

class User
{

    public string Username;
    public string Email;
    string _password;

    public User(string username, string email, string password)
    {
        Username = username;
        Email = email;
        _password = password;
    }
}