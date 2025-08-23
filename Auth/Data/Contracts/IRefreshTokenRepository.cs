using Auth.Common.Dtos;

namespace Auth.Data.Contracts
{
    public interface IRefreshTokenRepository
    {
        Task<string> CreateAsync(CreateRefreshTokenDto request);
    }
}