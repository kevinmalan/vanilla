using Core.Auth.Models;

namespace Core.Auth.Contracts
{
    public interface IPasswordService
    {
        Password HashPassword(string plainTextPsw);
    }
}