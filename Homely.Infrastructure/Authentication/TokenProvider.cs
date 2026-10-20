using System.Security.Claims;
using System.Security.Cryptography;
using Homely.Application.Authentication;
using Homely.Domain.Entities;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Homely.Infrastructure.Authentication;

public sealed class TokenProvider(JwtOptions jwt) : ITokenProvider
{
    public string GenerateAccessToken(User user)
    {
        var now = DateTime.UtcNow;
        var key = new SymmetricSecurityKey(Convert.FromBase64String(jwt.SecretBase64));

        var claims = new[]
        {
            new Claim("sub", user.Id.ToString()),
            new Claim("email", user.Email),
            new Claim("role", user.Role.ToString()),
            new Claim("jti", Guid.NewGuid().ToString("N"))
        };

        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            IssuedAt = now,
            NotBefore = now,
            Expires = now.AddMinutes(jwt.AccessTokenMinutes),
            Issuer = jwt.Issuer,
            Audience = jwt.Audience,
            SigningCredentials =
                new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        };

        return new JsonWebTokenHandler().CreateToken(descriptor);
    }

    public string GenerateRefreshToken()
        => Convert.ToHexString(RandomNumberGenerator.GetBytes(32));
}