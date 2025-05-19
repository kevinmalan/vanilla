namespace Data.Auth.Contracts
{
    public interface IAuthSeeder
    {
        Task SeedUsersAsync(Core.Auth.Models.User user, Core.Auth.Models.Password password);
    }
}