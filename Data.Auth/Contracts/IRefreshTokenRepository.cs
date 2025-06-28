using Common.Auth.Dtos;

namespace Data.Auth.Contracts
{
    public interface IRefreshTokenRepository
    {
        Task<string> CreateAsync(CreateRefreshTokenDto request);
    }
}