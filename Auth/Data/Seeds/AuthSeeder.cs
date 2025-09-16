using Auth.Data.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Auth.Data.Seeds
{
    public class AuthSeeder(AuthContext dataContext) : IAuthSeeder
    {
        public async Task SeedUsersAsync(Core.Models.User user, Core.Models.Password password)
        {
            var existingUser = await dataContext.Users.FirstOrDefaultAsync(x => x.Username == user.Username);
            if (existingUser != null)
                return;

            await dataContext.Users.AddAsync(new Auth.Data.Entities.User
            {
                UniqueId = user.UniqueId,
                Username = user.Username,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Status = user.Status,
                StatusReason = user.StatusReason,
                Role = user.Role,
                Password = password.Hash,
                Salt = password.Salt
            });

            await dataContext.SaveChangesAsync();
        }
    }
}