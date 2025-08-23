using Auth.Core.Contracts;
using Auth.Data.Contracts;

namespace Auth.Core.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        public async Task<Models.User> GetUserByUniqueIdAsync(Guid uniqueId)
        {
            return await userRepository.GetUserByUniqueIdAsync(uniqueId);
        }
    }
}