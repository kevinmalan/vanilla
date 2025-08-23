using Auth.Data.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Auth.Data.Repositories
{
    public class UserRepository(AuthContext context, IPasswordService passwordService) : IUserRepository
    {
        public async Task<Core.Models.User> GetUserByUsernameAndPasswordAsync(string username, string password)
        {
            var userEntity = await context.Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Username == username)
                    ?? throw new Exception($"No user found with username {username}");

            var isPasswordValid = passwordService.Verify(password, userEntity.Password, userEntity.Salt);

            if (!isPasswordValid)
                throw new Exception("Invalid username/password");

            return new Core.Models.User
            {
                Firstname = userEntity.Firstname,
                Lastname = userEntity.Lastname,
                Username = userEntity.Username,
                Role = userEntity.Role,
                Status = userEntity.Status,
                StatusReason = userEntity.StatusReason,
                UniqueId = userEntity.UniqueId
            };
        }

        public async Task<Core.Models.User> GetUserByUniqueIdAsync(Guid uniqueId)
        {
            var user = await context.Users
                    .AsNoTracking()
                    .Where(u => u.UniqueId == uniqueId)
                    .Select(u => new Core.Models.User
                    {
                        Firstname = u.Firstname,
                        Lastname = u.Lastname,
                        Username = u.Username,
                        Role = u.Role,
                        Status = u.Status,
                        StatusReason = u.StatusReason,
                        UniqueId = u.UniqueId
                    })
                    .FirstOrDefaultAsync();

            return user ?? throw new Exception($"No user found with uniqueId {uniqueId}");
        }
    }
}