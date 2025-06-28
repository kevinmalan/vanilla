using Core.Auth.Models;

namespace Data.Auth.Contracts
{
    public interface IPasswordService
    {
        Password HashPassword(string plainTextPsw);

        bool Verify(string plainTextPsw, string hash, string salt);

        bool ValidateStrength(string plainTextPsw);
    }
}