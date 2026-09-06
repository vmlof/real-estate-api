using Homely.Domain.Enums;

namespace Homely.Domain.Entities;

public class User
{
    public int Id { get; private set; }
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public Role Role { get; private set; }

    public User(string email, string passwordHash, Role role)
    {
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
    }
}