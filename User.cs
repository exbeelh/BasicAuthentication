namespace AuthenticationApp;

class User
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string FullName { get; set; }
    public string Password { get; set; }

    public User (string firstName, string lastName, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        Username = $"{firstName[..2]}{lastName[..2]}";
        FullName = $"{firstName} {lastName}";
        Password = password;
    }

    public User ()
    {

    }
}