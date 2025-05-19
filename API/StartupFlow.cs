using Core.Auth.Contracts;
using Data.Auth.Contracts;

namespace API
{
    public class StartupFlow(IPasswordService passwordService, IAuthSeeder authSeeder)
    {
        public async Task SeedUserAsync()
        {
            var user = new Core.Auth.Models.User
            {
                Username = "admin@vanilla.com",
                Firstname = "Foo",
                Lastname = "Bar",
                UniqueId = Guid.NewGuid(),
                Status = Common.Auth.Enums.UserStatus.Active,
                Role = Common.Auth.Enums.UserRole.Admin
            };

            var hashedPassword = passwordService.HashPassword("1ncrediblyStongP@ssword!#");
            var password = new Core.Auth.Models.Password
            {
                Hash = hashedPassword.Hash,
                Salt = hashedPassword.Salt
            };

            await authSeeder.SeedUsersAsync(user, password);
        }
    }
}