using Core.Auth.Services;
using Core.Models;
using Microsoft.Extensions.Options;
using NSubstitute;
using Shared.Config;
using Shouldly;

namespace Tests.Auth
{
    public class TokenTests
    {
        [Fact]
        public void CreateAccessToken_ShouldReturnValue()
        {
            // Arrange
            var options = Substitute.For<IOptions<TokenConfig>>();
            options.Value.Returns(new TokenConfig
            {
                Access = new AccessTokenConfig
                {
                    Secret = $"{Guid.NewGuid()}",
                    Expires = new TimeSpan(01, 00, 00)
                },
                Issuer = "https://api.issuerApp.com",
                Audience = "https://api.audianceApp.com"
            });
            var user = new User
            {
                Firstname = "f",
                Lastname = "l",
                Role = Shared.Enums.UserRole.Admin,
                Status = Shared.Enums.UserStatus.Active,
                UniqueId = Guid.NewGuid(),
                Username = "u@email.com"
            };
            var service = new TokenService(options);

            // Act
            var accessToken = service.CreateAccessToken(user);

            // Assert
            accessToken.ShouldNotBeNullOrWhiteSpace();
            $"{accessToken[0]}{accessToken[1]}".ShouldBe("ey");
        }
    }
}