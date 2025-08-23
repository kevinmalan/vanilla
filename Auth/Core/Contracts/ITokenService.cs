namespace Auth.Core.Contracts
{
    public interface ITokenService
    {
        string CreateAccessToken(Models.User user);
    }
}