using Core.Auth.Config;
using Core.Auth.Services;
using Microsoft.Extensions.Options;
using NSubstitute;
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
    }
}