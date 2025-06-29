namespace Core.Auth.Contracts
{
    public interface IUserService
    {
        Task<Models.User> GetUserByUniqueIdAsync(Guid uniqueId);
    }
}