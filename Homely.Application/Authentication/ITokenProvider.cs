using System.Security.Claims;
using Homely.Domain.Entities;

namespace Homely.Application.Authentication;

public interface ITokenProvider
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
}

// public interface ITokenProvider
// {
//     (string AccessToken, string RefreshToken) GenerateTokens(User user);
// }