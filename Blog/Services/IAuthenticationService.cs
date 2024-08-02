namespace BlogApp.Services
{
    public interface IAuthenticationService
    {
        string Authenticate(string username, string password);
    }
}