namespace Data.Auth.Contracts
{
    public interface IUserRepository
    {
        Task<Core.Auth.Models.User> GetUserByUsernameAndPasswordAsync(string username, string password);
        Task<Core.Auth.Models.User> GetUserByUniqueIdAsync(Guid uniqueId);
    }
}