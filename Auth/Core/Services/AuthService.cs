using Auth.Core.Contracts;
using Auth.Data.Contracts;
using Microsoft.AspNetCore.Http;

namespace Auth.Core.Services
{
    public class AuthService(
        IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository,
        ITokenService tokenService,
        IHttpContextAccessor httpContextAccessor)
        : IAuthService
    {
        public async Task LoginAsync(string username, string password)
        {
            var user = await userRepository.GetUserByUsernameAndPasswordAsync(username, password);
            var accessToken = tokenService.CreateAccessToken(user);
            var refreshToken = await refreshTokenRepository.CreateAsync(new Common.Dtos.CreateRefreshTokenDto
            {
                UniqueUserId = user.UniqueId,
                ClientIp = "10.0.0.5", // TODO pass through form client
                Expires = new TimeSpan(7, 0, 0, 0) // TODO read from config
            });

            // TODO: Call a Service to set RevokedAt and RevokedByIp for all prev refreshtokens for this user. Excluding the current one

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None, // So the Vite UI running on another domain can access it
                Expires = DateTime.UtcNow.AddDays(7) // TODO read from config
            };

            httpContextAccessor.HttpContext.Response.Cookies.Append("access_token", accessToken, cookieOptions);
            httpContextAccessor.HttpContext.Response.Cookies.Append("refresh_token", refreshToken, cookieOptions);
        }
    }
}