using Core.Auth.Contracts;
using Core.Auth.Models;
using Konscious.Security.Cryptography;
using Microsoft.Extensions.Options;
using Shared.Config;
using System.Security.Cryptography;
using System.Text;

namespace Core.Auth.Services
{
    public class PasswordService(IOptions<PasswordConfig> passwordOptions) : IPasswordService
    {
        private readonly PasswordConfig _passwordConfig = passwordOptions.Value;

        public Password HashPassword(string plainTextPsw)
        {
            var saltBytes = RandomNumberGenerator.GetBytes(_passwordConfig.SaltLength);
            var pepperBytes = Encoding.UTF8.GetBytes(_passwordConfig.Pepper);
            var plainTextPswBytes = Encoding.UTF8.GetBytes(plainTextPsw);
            var argon = new Argon2id(plainTextPswBytes)
            {
                Salt = saltBytes,
                DegreeOfParallelism = _passwordConfig.Parallelism,
                MemorySize = _passwordConfig.MemoryKb,
                Iterations = _passwordConfig.Iterations,
                KnownSecret = pepperBytes
            };
            var hashBytes = argon.GetBytes(_passwordConfig.HashLength);

            return new Password
            {
                Hash = Convert.ToBase64String(hashBytes),
                Salt = Convert.ToBase64String(saltBytes)
            };
        }

        public bool Verify(string plainTextPsw, string storedHash, string storedSalt)
        {
            var saltBytes = Convert.FromBase64String(storedSalt);
            var pepperBytes = Encoding.UTF8.GetBytes(_passwordConfig.Pepper);
            var expected = Convert.FromBase64String(storedHash);
            var argon = new Argon2id(Encoding.UTF8.GetBytes(plainTextPsw))
            {
                Salt = saltBytes,
                DegreeOfParallelism = _passwordConfig.Parallelism,
                MemorySize = _passwordConfig.MemoryKb,
                Iterations = _passwordConfig.Iterations,
                KnownSecret = pepperBytes
            };
            var actual = argon.GetBytes(expected.Length);

            return CryptographicOperations.FixedTimeEquals(actual, expected);
        }

        public bool ValidateStrength(string plainTextPsw)
        {
            const int minLength = 12;

            if (string.IsNullOrWhiteSpace(plainTextPsw) || plainTextPsw.Length < minLength)
                return false;

            var hasUpper = false;
            var hasLower = false;
            var hasDigit = false;
            var hasSymbol = false;

            foreach (var c in plainTextPsw)
            {
                if (char.IsWhiteSpace(c))
                    return false;

                if (char.IsUpper(c))
                    hasUpper = true;

                if (char.IsLower(c))
                    hasLower = true;

                if (char.IsDigit(c))
                    hasDigit = true;

                if (!char.IsLetterOrDigit(c))
                    hasSymbol = true;
            }

            return hasUpper && hasLower && hasDigit && hasSymbol;
        }
    }
}