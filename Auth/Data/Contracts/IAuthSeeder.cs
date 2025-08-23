namespace Auth.Data.Contracts
{
    public interface IAuthSeeder
    {
        Task SeedUsersAsync(Core.Models.User user, Core.Models.Password password);
    }
}