using Common.Auth.Dtos;
using Data.Auth.Contracts;
using Data.Auth.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Data.Auth.Repositories
{
    public class RefreshTokenRepository(AuthContext context) : IRefreshTokenRepository
    {
        public async Task<string> CreateAsync(CreateRefreshTokenDto request)
        {
            var randomBytes = RandomNumberGenerator.GetBytes(64);
            var refreshTokenRaw = Convert.ToBase64String(randomBytes);

            var tokenHashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(refreshTokenRaw));
            var tokenHash = Convert.ToBase64String(tokenHashBytes);

            var userId = await context.Users
                .AsNoTracking()
                .Where(x => x.UniqueId == request.UniqueUserId)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            var refreshToken = new RefreshToken
            {
                TokenHash = tokenHash,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.Add(request.Expires),
                CreatedByIp = request.ClientIp,
                UserId = userId
            };

            await context.RefreshTokens.AddAsync(refreshToken);
            await context.SaveChangesAsync();

            return refreshTokenRaw;
        }
    }
}