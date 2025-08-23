namespace Auth.Core.Contracts
{
    public interface IUserService
    {
        Task<Models.User> GetUserByUniqueIdAsync(Guid uniqueId);
    }
}