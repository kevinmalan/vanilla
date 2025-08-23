using Auth.Core.Models;

namespace Auth.Data.Contracts
{
    public interface IPasswordService
    {
        Password HashPassword(string plainTextPsw);

        bool Verify(string plainTextPsw, string hash, string salt);

        bool ValidateStrength(string plainTextPsw);
    }
}