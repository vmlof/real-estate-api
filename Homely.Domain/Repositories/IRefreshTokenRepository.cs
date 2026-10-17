using Homely.Domain.Entities;

namespace Homely.Domain.Repositories;

public interface IRefreshTokenRepository
{
    Task<RefreshToken> GetRefreshToken(string refreshToken);
}