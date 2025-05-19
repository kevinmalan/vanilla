using Core.Auth.Services;
using Microsoft.Extensions.Options;
using NSubstitute;
using Common.Auth.Config;
using Shouldly;

namespace Tests.Auth
{
    public class PasswordTests
    {
        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData(" ", false)]
        [InlineData("               ", false)]
        [InlineData("pass123", false)]
        [InlineData("Short1!", false)]
        [InlineData("Loooooooooong2", false)]
        [InlineData("Loooooooooong!", false)]
        [InlineData("loooooooooong@!", false)]
        [InlineData("Looooooong2!", true)]
        [InlineData("Inv@lidP@ssword", false)]
        [InlineData("Almost V@lidP@ssword15", false)]
        [InlineData("V@lidP@ssword15", true)]
        public void ValidateStrength_ReturnsExpected(string password, bool expected)
        {
            // Arrange
            var options = Substitute.For<IOptions<PasswordConfig>>();
            options.Value.Returns(new PasswordConfig
            {
                SaltLength = 16,
                HashLength = 32,
                MemoryKb = 65536,
                Iterations = 4,
                Parallelism = 2,
                Pepper = $"{Guid.NewGuid()}"
            });
            var service = new PasswordService(options);

            // Act
            var result = service.ValidateStrength(password);

            // Assert
            result.ShouldBe(expected);
        }

        [Theory]
        [InlineData("a", "b", false)]
        [InlineData("111", "11", false)]
        [InlineData("V@lidP@ssword15", "V@lidP@ssword14", false)]
        [InlineData("V@lidP@ssword15", "V@lidP@ssword15", true)]
        public void Hash_Verify_ReturnsExpected(string password1, string password2, bool expected)
        {
            // Arrange
            var options = Substitute.For<IOptions<PasswordConfig>>();
            options.Value.Returns(new PasswordConfig
            {
                SaltLength = 16,
                HashLength = 32,
                MemoryKb = 65536,
                Iterations = 4,
                Parallelism = 2,
                Pepper = $"{Guid.NewGuid()}"
            });
            var service = new PasswordService(options);

            // Act
            var storedPassword = service.HashPassword(password1);
            var storedPasswordHash = storedPassword.Hash;
            var storedSaltHash = storedPassword.Salt;
            var verifiedResult = service.Verify(password2, storedPasswordHash, storedSaltHash);

            // Assert
            verifiedResult.ShouldBe(expected);
        }
    }
}