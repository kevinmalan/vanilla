using Data.Auth.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Data.Auth.Seeds
{
    public class AuthSeeder(AuthContext dataContext) : IAuthSeeder
    {
        public async Task SeedUsersAsync(Core.Auth.Models.User user, Core.Auth.Models.Password password)
        {
            var existingUser = await dataContext.Users.FirstOrDefaultAsync();
            if (existingUser != null)
                return;

            await dataContext.Users.AddAsync(new Entities.User
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