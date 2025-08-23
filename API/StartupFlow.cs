using Auth.Data.Contracts;

namespace API
{
    public class StartupFlow(IPasswordService passwordService, IAuthSeeder authSeeder)
    {
        public async Task SeedUserAsync()
        {
            var user = new Auth.Core.Models.User
            {
                Username = "admin@vanilla.com",
                Firstname = "Foo",
                Lastname = "Bar",
                UniqueId = Guid.NewGuid(),
                Status = Auth.Common.Enums.UserStatus.Active,
                Role = Auth.Common.Enums.UserRole.Admin
            };

            var hashedPassword = passwordService.HashPassword("1ncrediblyStongP@ssword!#");
            var password = new Auth.Core.Models.Password
            {
                Hash = hashedPassword.Hash,
                Salt = hashedPassword.Salt
            };

            await authSeeder.SeedUsersAsync(user, password);
        }
    }
}