namespace Core.Auth.Contracts
{
    public interface IAuthService
    {
        Task LoginAsync(string username, string password);
    }
}