namespace Auth.Core.Contracts
{
    public interface IAuthService
    {
        Task LoginAsync(string username, string password);
    }
}