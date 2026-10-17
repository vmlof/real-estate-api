using Homely.Domain.Entities;

namespace Homely.Application.Authentication;

public interface ITokenProvider
{
    (string AccessToken, string RefreshToken) GenerateTokens(User user);
}