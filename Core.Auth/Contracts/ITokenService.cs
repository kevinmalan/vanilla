namespace Core.Auth.Contracts
{
    public interface ITokenService
    {
        string CreateAccessToken(Models.User user);
    }
}