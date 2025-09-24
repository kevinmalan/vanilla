namespace Auth.Data.Contracts
{
    public interface IUserRepository
    {
        Task<Auth.Core.Models.User> GetUserByUsernameAndPasswordAsync(string username, string password);
        Task<Auth.Core.Models.User> GetUserByUniqueIdAsync(Guid uniqueId, CancellationToken cancellationToken);
    }
}