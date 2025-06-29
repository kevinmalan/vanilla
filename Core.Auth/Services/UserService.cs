using Core.Auth.Contracts;
using Data.Auth.Contracts;

namespace Core.Auth.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        public async Task<Models.User> GetUserByUniqueIdAsync(Guid uniqueId)
        {
            return await userRepository.GetUserByUniqueIdAsync(uniqueId);
        }
    }
}