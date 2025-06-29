using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Common.Auth.Config;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Auth.Contracts;

namespace Core.Auth.Services
{
    public class TokenService(IOptions<TokenConfig> tokenOptions) : ITokenService
    {
        private readonly TokenConfig _tokenConfig = tokenOptions.Value;

        public string CreateAccessToken(Models.User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, $"{Guid.NewGuid()}"),
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(ClaimTypes.Role, $"{user.Role}"),
                new Claim("unique-id", $"{user.UniqueId}"),
                new Claim("status", $"{user.Status}")
            };

            var secretBytes = Encoding.UTF8.GetBytes(_tokenConfig.Access.Secret);
            var key = new SymmetricSecurityKey(secretBytes);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                issuer: _tokenConfig.Issuer,
                audience: _tokenConfig.Audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(_tokenConfig.Access.Expires),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}