using Core.Auth.Contracts;
using Data.Contracts;

namespace API
{
    public class StartupFlow(IPasswordService passwordService, IAuthSeeder authSeeder)
    {
        public async Task SeedUserAsync()
        {
            var user = new Core.Models.User
            {
                Username = "admin@vanilla.com",
                Firstname = "Foo",
                Lastname = "Bar",
                UniqueId = Guid.NewGuid(),
                Status = Shared.Enums.UserStatus.Active,
                Role = Shared.Enums.UserRole.Admin
            };

            var hashedPassword = passwordService.HashPassword("1ncrediblyStongP@ssword!#");
            var password = new Core.Models.Password
            {
                Hash = hashedPassword.Hash,
                Salt = hashedPassword.Salt
            };

            await authSeeder.SeedUsersAsync(user, password);
        }
    }
}