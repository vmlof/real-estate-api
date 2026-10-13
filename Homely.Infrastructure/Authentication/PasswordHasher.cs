using System.Security.Cryptography;
using Homely.Application.Interfaces;

namespace Homely.Infrastructure.Authentication;

public sealed class PasswordHasher : IPasswordHasher
{
    private const int WorkFactor = 13;

    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password, WorkFactor);
    }

    public bool Verify(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash);
    }
}